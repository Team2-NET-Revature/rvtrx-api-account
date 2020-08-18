using RVTR.Account.ObjectModel.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RVTR.Account.DataContext
{
    /// <summary>
    /// Represents the _Account_ context
    /// </summary>
    public static class SeedDb
    {
        public static void Seed(AccountContext ctx)
        {
            if (ctx.Accounts.Count() > 0)
            {
                ctx.Accounts.RemoveRange(ctx.Accounts);
                ctx.SaveChanges();
            }
            ctx.Accounts.Add(new AccountModel
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
                                    cardExpirationDate = new DateTime(),
                                    cardNumber = "xxxx-xxxx-xxxx-1234",
                                    cardName = "Visa",
                                    securityCode = "123"
                                }
                            },
                Profiles = new List<ProfileModel>()
                        {
                            new ProfileModel()
                            {
                                Id = -1,
                                Email = "Test@test.com",
                                familyName = "Jones",
                                givenName = "Tom",
                                Phone = "1234567891",
                                Type = "Adult",
                                AccountId = -1
                            }
                        }
            });
            ctx.SaveChanges();
        }
    }
}