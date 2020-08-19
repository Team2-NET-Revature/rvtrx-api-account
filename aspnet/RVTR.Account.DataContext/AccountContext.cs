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
      modelBuilder.Entity<BankCardModel>().HasKey(e => e.Id);
      modelBuilder.Entity<PaymentModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ProfileModel>().HasKey(e => e.Id);

    }
  }
}