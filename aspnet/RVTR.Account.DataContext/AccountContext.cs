using System;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext
{
  /// <summary>
  /// Represents the _Account_ context
  /// </summary>
  public class AccountContext : DbContext
  {
    public DbSet<AccountModel> Accounts { get; set; }
    public DbSet<ProfileModel> Profiles { get; set; }
    public DbSet<PaymentModel> Payments { get; set; }
    public DbSet<AddressModel> Addresses { get; set; }

    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AccountModel>().HasKey(e => e.Id);
      modelBuilder.Entity<AddressModel>().HasKey(e => e.Id);
      modelBuilder.Entity<PaymentModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ProfileModel>().HasKey(e => e.Id);
      modelBuilder.Entity<AccountModel>().HasData
      (
        new AccountModel
        {
          Id = -1,
          Name = "Name",
          Email = "Test@test.com"
        },
        new AccountModel()
        {
          Id = 1,
          Name = "Camper",
          Email = "demo.camper@revature.com"
        }
      );
      modelBuilder.Entity<PaymentModel>().HasData
      (
        new PaymentModel()
        {
          Id = -1,
          CardExpirationDate = new DateTime(),
          CardNumber = "1234123412341234",
          CardName = "Visa",
          SecurityCode = "123",
          AccountId = -1
        },
        new PaymentModel()
        {
          Id = 1,
          AccountId = 1,
          CardExpirationDate = new System.DateTime(2020, 08, 31),
          CardNumber = "4111111111111111",
          SecurityCode = "123",
          CardName = "User's credit card"
        }
      );
      modelBuilder.Entity<AddressModel>().HasData
      (
        new AddressModel()
        {
          Id = -1,
          City = "City",
          Country = "USA",
          PostalCode = "21345",
          StateProvince = "NC",
          Street = "123 elm street",
          AccountId = -1,
        },
        new AddressModel()
        {
          Id = 1,
          AccountId = 1,
          City = "Austin",
          Country = "USA",
          PostalCode = "73301",
          StateProvince = "TX",
          Street = "Test St"
        }
      );
      modelBuilder.Entity<ProfileModel>().HasData
      (
        new ProfileModel()
        {
          Id = -1,
          Email = "Test@test.com",
          FamilyName = "Jones",
          GivenName = "Tom",
          Phone = "1234567891",
          Type = "Adult",
          AccountId = -1
        },
        new ProfileModel()
        {
          Id = 1,
          AccountId = 1,
          Email = "demo.camper@revature.com",
          FamilyName = "FamilyName",
          GivenName = "GivenName",
          Phone = "123-456-7891",
          Type = "Type"
        }
      );
    }
  }
}
