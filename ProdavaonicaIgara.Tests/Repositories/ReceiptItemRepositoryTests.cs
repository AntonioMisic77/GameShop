using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
    public class ReceiptItemRepositoryTests
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

            context.Articles.AddRange(
                new Article
                {
                    Id = 1,
                    Name = "Article1",
                    Description = "Description1",
                    Price = 100,
                    SupplierId = 1
                },
                new Article
                {
                    Id = 2,
                    Name = "Article2",
                    Description = "Description2",
                    Price = 200,
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

            var receiptItem = new ReceiptItem
            {
                Id = 1,
                ArticleId = 1,
                ReceiptId = 1,
                Quantity = 1
            };
            context.ReceiptItems.Add(receiptItem);

            context.SaveChanges();

            context.Entry(receiptItem).State = EntityState.Detached;

        }
        #endregion

        #region tests

        [Fact]
        public async void ReceiptItemRepository_CreateReceiptItem_CreatesReceiptItem()
        {
            var context = GetDbContext();
            initalizeDb(context);

            var receiptItem = new ReceiptItem
            {
                Id = 2,
                ArticleId = 2,
                ReceiptId = 1,
                Quantity = 1
            };

            var receiptItemRepository = new ReceiptItemRepository(new GenericRepository<ReceiptItem>(context),context);

            var createdReceiptItem = await receiptItemRepository.CreateAsync(receiptItem);

            Assert.NotNull(createdReceiptItem);
            Assert.Equal(receiptItem.Id, createdReceiptItem.Id);
            Assert.Equal(receiptItem.ArticleId, createdReceiptItem.ArticleId);
            Assert.Equal(receiptItem.ReceiptId, createdReceiptItem.ReceiptId);
            Assert.Equal(receiptItem.Quantity, createdReceiptItem.Quantity);

            Assert.Equal(2, context.ReceiptItems.Count());
        }


        [Fact]
        public async void ReceiptItemRepository_UpdateReceiptItem_UpdatesReceiptItem()
        {
            var context = GetDbContext();
            initalizeDb(context);

            var receiptItem = new ReceiptItem
            {
                Id = 1,
                ArticleId = 1,
                ReceiptId = 1,
                Quantity = 2
            };

            var receiptItemRepository = new ReceiptItemRepository(new GenericRepository<ReceiptItem>(context), context);

            var updatedReceiptItem = await receiptItemRepository.UpdateAsync(receiptItem);

            Assert.NotNull(updatedReceiptItem);
            Assert.Equal(receiptItem.Id, updatedReceiptItem.Id);
            Assert.Equal(receiptItem.ArticleId, updatedReceiptItem.ArticleId);
            Assert.Equal(receiptItem.ReceiptId, updatedReceiptItem.ReceiptId);
            Assert.Equal(receiptItem.Quantity, updatedReceiptItem.Quantity);
        }


        [Fact]
        public async void ReceiptItemRepository_DeleteReceiptItem_DeletesReceiptItem()
        {
            var context = GetDbContext();
            initalizeDb(context);

            var receiptItemRepository = new ReceiptItemRepository(new GenericRepository<ReceiptItem>(context), context);

            var deletedReceiptItem = await receiptItemRepository.DeleteAsync(1);

            Assert.NotNull(deletedReceiptItem);
            Assert.Equal(0, context.ReceiptItems.Count());
        }

        #endregion 
    }
}
