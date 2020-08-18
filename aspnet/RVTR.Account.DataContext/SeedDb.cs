using RVTR.Account.ObjectModel.Models;
using System.Collections.Generic;
using System;

namespace RVTR.Account.DataContext
{
    /// <summary>
    /// Represents the _Account_ context
    /// </summary>
    public static class SeedDb
    {
			public static void Seed(AccountContext ctx){
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
									Expiry = new DateTime(),
									Number = "xxxx-xxxx-xxxx-1234",
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
						ctx.SaveChanges();
					}
			}
}