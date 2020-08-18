using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RVTR.Account.DataContext;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.WebApi
{
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            var host = CreateHostBuilder().Build();

            await CreateDbContextAsync(host);
            await host.RunAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder() =>
          Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder.UseStartup<Startup>();
          });

        /// <summary>
        ///
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static async Task CreateDbContextAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<AccountContext>();

                await context.Database.EnsureCreatedAsync();
                context.Accounts.Add(new AccountModel
                {

                    Id = -1,
                    Address = new AddressModel()
                    {
                        Id = -1,
                        City = "City",
                        Country = "Country",
                        PostalCode = "21345",
                        StateProvince = "Somewhere",
                        Street = "123 elm street",
                        AccountId = -1,

                    },
                    Name = "Name",
                    Payments = new List<PaymentModel>()
        {
          new PaymentModel()
          {
            Id = -1,
            BankCard = new BankCardModel()
            {
              Id = -1,
              Expiry = new DateTime(),
              Number = "xxxx-xxxx-xxxx-1234"
            },
            Name = "Visa"
          }
        },


                    Profiles = new List<ProfileModel>()
        {
          new ProfileModel()
          {
            Id = -1,
            Email = "Test@test.com",
            Family = "Jones",
            Given = "Tom",
            Phone = "1234567891",
            AccountId = -1
          }
        }
                });
                context.SaveChanges();
            }
        }
    }
}
