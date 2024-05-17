using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdavaonicaIgara.Tests.Repositories
{
    public class ReceiptRepositoryTests
    {
        private PIGDbContext GetDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var dbOptions = new DbContextOptionsBuilder<PIGDbContext>()
                            .UseSqlite(connection)
                            .Options;

            var dbContext = new PIGDbContext(dbOptions);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        private void initalizeDb(PIGDbContext context)
        {
            context.Receipts.AddRangeAsync(new List<Receipt>
            {
                new Receipt
                {
                    Id = 1,
                    PaymentMethod = "Card",
                    ReceiptItems = new List<ReceiptItem>
                    {
                        new ReceiptItem
                        {
                            Id = 1,
                            ArticleId = 1,
                            Article = new Article
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
                }
            });

            context.SaveChanges();
        }

        [Fact]
        public void nesto()
        {
            var _context = GetDbContext();
           // initalizeDb(_context);
        }
    }
}
