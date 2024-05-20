using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Configurations;
using ProdavaonicaIgaraAPI.Data.Articles;
using ProdavaonicaIgaraAPI.Data.Pageing;
using ProdavaonicaIgaraAPI.Data.Receipt;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdavaonicaIgara.Tests.Repositories
{
    public class ReceiptRepositoryTests
    {
        #region metods
        private PIGDbContext GetDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var dbOptions = new DbContextOptionsBuilder<PIGDbContext>()
                            .UseSqlite(connection)
                            .EnableSensitiveDataLogging()
                            .Options;

            var dbContext = new PIGDbContext(dbOptions);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        private void initalizeDb(PIGDbContext context)
        {
            context.Suppliers.Add(
                new Supplier
                {
                    Id = 1,
                    Name = "Supplier1",
                    Address = "Address1",
                    Email = "Email1",
                }
            );

            context.SaveChanges();

            context.Articles.Add(
                new Article
                {
                    Id = 1,
                    Name = "Article1",
                    Description = "Description1",
                    Price = 100,
                    SupplierId = 1
                }
            );

            context.SaveChanges();

            context.Companies.Add(new Company
            { 
                Id = 1,
                Name = "Company1",
                Address = "Address1",
                Email = "Email1",
                Phone = "Phone1"
            });

            context.Users.Add(new User()
            {
                Id = 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                Username = "Username1",
                Email = "Email1",
                PasswordHash = "Password1",
            }); 

            context.SaveChanges();

            var receipt = new Receipt
            {
                Id = 1,
                CashierId = 1,
                CompanyId = 1,
                PaymentMethod = "Card",
                Date = DateTime.Now
            };

            context.Receipts.Add(receipt);

            context.SaveChanges();

            context.Entry(receipt).State = EntityState.Detached;

            context.ReceiptItems.Add(
                new ReceiptItem
                {
                    Id = 1,
                    ArticleId = 1,
                    ReceiptId = 1,
                    Quantity = 1
                }
            );

            context.SaveChanges();

        }
        #endregion

        #region tests
        [Fact]
        public async void ReceiptRepository_GetPagedReceipts_ReturnsPagedReciptesWithRecipteItems()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var expectedResult = new Receipt()
            {
                Id = 1,
                PaymentMethod = "Card",
                Date = DateTime.Now,
                ReceiptItems = new List<ReceiptItem>
                {
                    new ReceiptItem
                    {
                        Id = 1,
                        ArticleId = 1,
                        ReceiptId = 1,
                        Article = new Article()
                        {
                            Id = 1,
                            Name = "Article1",
                            Description = "Description1",
                            Price = 100,
                            SupplierId = 1
                        },
                        Quantity = 1
                    }
                }
            };

            var repository = new ReceiptRepository(new GenericRepository<Receipt>(_context), _context);


            var result = await repository.GetPagedReceipts(new QueryParametars { PageSize = 10, StartIndex = 0 });


            Assert.Equal(result.Count,1);

            Assert.Equal(expectedResult.Id, result[0].Id);
            Assert.Equal(expectedResult.PaymentMethod, result[0].PaymentMethod);

            Assert.Equal(result[0].ReceiptItems.Count(),expectedResult.ReceiptItems.Count());

        }

        [Fact]
        public async void ReceiptRepository_CreateReceipt_CreatesReceiptAndReturnsReceipt()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var repository = new ReceiptRepository(new GenericRepository<Receipt>(_context), _context);

            var receipt = new Receipt()
            {
                CashierId = 1,
                CompanyId = 1,
                PaymentMethod = "Card",
                Date = DateTime.Now,
                ReceiptItems = new List<ReceiptItem>
                {
                    new ReceiptItem
                    {
                        ArticleId = 1,
                        Quantity = 1
                    }
                }
            };

            var result = await repository.CreateAsync(receipt);

            Assert.NotNull(result);
            Assert.Equal(receipt.CashierId, result.CashierId);
            Assert.Equal(receipt.CompanyId, result.CompanyId);
            Assert.Equal(receipt.PaymentMethod, result.PaymentMethod);

            Assert.Equal(_context.Receipts.AsNoTracking().Count(), 2);
            
        }

        [Fact]
        public async void ReceiptRepository_UpdateReceipt_UpdatesReceiptAndReturnsReceipt()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var repository = new ReceiptRepository(new GenericRepository<Receipt>(_context), _context);

            var newReceipt = new Receipt()
            {
                Id = 1,
                PaymentMethod = "Cash",
                CashierId = 1,
                CompanyId = 1,
                Date = DateTime.Now,
            };

            var oldReceipt = _context.Receipts.AsNoTracking().FirstOrDefault(a => a.Id == 1);

            var result = await repository.UpdateAsync(newReceipt);

            Assert.NotNull(result);
            Assert.NotEqual(oldReceipt.PaymentMethod, result.PaymentMethod);
        }

        [Fact]
        public async void ReceiptRepository_DeleteReceipt_DeletesReceipt()
        {
            var _context = GetDbContext();
            initalizeDb(_context);

            var repository = new ReceiptRepository(new GenericRepository<Receipt>(_context), _context);

            await repository.DeleteAsync(1);

            Assert.Equal(_context.Receipts.AsNoTracking().Count(), 0);
        }

        #endregion
    }
}
