using AngleSharp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

//var application = new WebApplicationFactory<Program>()
//    .WithWebHostBuilder(builder =>
//    {
//        builder.ConfigureServices(services =>
//        {
//            // Удаляем текущую конфигурацию базы данных
//            var descriptor = services.SingleOrDefault(
//                d => d.ServiceType == typeof(DbContextOptions<CatalogContext>));
//            if (descriptor != null) services.Remove(descriptor);

//            // Добавляем базу данных в памяти
//            services.AddDbContext<CatalogContext>(options =>
//            {
//                options.UseInMemoryDatabase("TestDatabase");
//            });
//        });

//        builder.UseEnvironment("Test");
//    });

//_client = application.CreateClient();
//_context = application.Services.GetRequiredService<CatalogContext>();
namespace KT4
{
    //[TestFixture]
    [TestFixture]
    public class IntegrationTests
    {
        private HttpClient _client;
        string connectionString = "Server=localhost,1434;Database=Microsoft.eShopOnWeb.CatalogDb;User Id=sa;Password=@someThingComplicated1234;Encrypt=False;TrustServerCertificate=True;";

        [SetUp]
        public void Setup()
        {
            // Строка подключения к реальной базе данных

          

            // Инициализация HttpClient (если нужно)
            _client = new HttpClient();
        }

        [Test]
        public async Task Test_AddCatalogItem_negative()
        {
            try
            {
                var connectionString = "Server=localhost,1434;Database=Microsoft.eShopOnWeb.CatalogDb;User Id=sa;Password=@someThingComplicated1234;Encrypt=False;TrustServerCertificate=True;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        string squre_update = "insert into [dbo].[Catalog] ([Id],[Name],[Description],[Price],[PictureUri],[CatalogTypeId],[CatalogBrandId]) \r\n" +
                             "VALUES ('14','Product Lemon', 'A sample catalog item', 10.1, 'http://catalogbaseurltobereplaced/images/products/5.png', 3, 5);\r\n";
                        using (SqlCommand command = new SqlCommand(squre_update, connection))
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} row(s) inserted.");
                        }


                    }
                    catch (Exception EX)
                    {
                        Assert.Fail(EX.Source);

                        Console.WriteLine($"Error: {EX.Message}");
                    }
                }

            }

            catch (Exception EX)
            {
                Assert.Fail( EX.Source);

                Console.WriteLine($"Error: {EX.Message}");
            }




        }
        [Test]
        public async Task Test_AddCatalogItem()
        {
            try
            {
                var connectionString = "Server=localhost,1434;Database=Microsoft.eShopOnWeb.CatalogDb;User Id=sa;Password=@someThingComplicated1234;Encrypt=False;TrustServerCertificate=True;";

                string path = Environment.CurrentDirectory + "\\Table.sql";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        string squre_update = "insert into [dbo].[Catalog] ([Id],[Name],[Description],[Price],[PictureUri],[CatalogTypeId],[CatalogBrandId]) \r\n" +
                             "VALUES (13,'Product Lemon', 'A sample catalog item', 10.1, 'http://catalogbaseurltobereplaced/images/products/5.png', 3, 5);\r\n";
                        //string SQURE = "insert into [dbo].[Catalog] ([Name],[Description],[Price],[PictureUri],[CatalogTypeId],[CatalogBrandId])VALUES ('Prodcuct Lemon','A sample catalog item',10.1,'http://catalogbaseurltobereplaced/images/products/5.png','3','5');";
                        connection.Open();
                        Console.WriteLine("Connection to SQL Server established.");


                        using (SqlCommand command = new SqlCommand(squre_update, connection))
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} row(s) inserted.");
                        }
                    }
                    catch (Exception EX)
                    {
                        Console.WriteLine($"Error: {EX.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Очистка контекста
            //_context.Dispose();

            // Очистка HttpClient
            _client.Dispose();
        }
    }
}

