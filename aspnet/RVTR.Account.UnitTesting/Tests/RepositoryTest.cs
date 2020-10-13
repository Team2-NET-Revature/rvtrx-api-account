using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using Xunit;
using System.Linq;

namespace RVTR.Account.UnitTesting.Tests
{
  public class RepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;
    private AccountModel account = new AccountModel() { Id = 3 };
    private ProfileModel profile = new ProfileModel(){familyName = "FN", givenName = "GN",Id = 3,Email = "anemail@random.com",Phone = "123456789",Type = ""};
    private AddressModel address = new AddressModel() { Id = 3, AccountId = 3 };

    [Fact]
    public async void Test_Repository_DeleteAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using(var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }
        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);
          var sut = await ctx.Profiles.FirstAsync();
          await profiles.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.DoesNotContain(sut, await ctx.Profiles.ToListAsync());
        }

        using (var ctx = new AccountContext(_options))
        {
          var addresses = new Repository<AddressModel>(ctx);
          var sut = await ctx.Addresses.FirstAsync();
          await addresses.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.DoesNotContain(sut,await ctx.Addresses.ToListAsync());
        }

        using (var ctx = new AccountContext(_options))
        {
          var lodgings = new Repository<AccountModel>(ctx);
          var sut = await ctx.Accounts.FirstAsync();
          await lodgings.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.DoesNotContain(sut,await ctx.Accounts.ToListAsync());
        }

      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_InsertAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var lodgings = new Repository<AccountModel>(ctx);

          await lodgings.InsertAsync(account);
          await ctx.SaveChangesAsync();

          Assert.Contains(account,await ctx.Accounts.ToListAsync());
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);
          await profiles.InsertAsync(profile);
          await ctx.SaveChangesAsync();

          Assert.Contains(profile, await ctx.Profiles.ToListAsync());
        }

        using (var ctx = new AccountContext(_options))
        {
          var addreses = new Repository<AddressModel>(ctx);
          await addreses.InsertAsync(address);
          await ctx.SaveChangesAsync();

          Assert.Contains(address,await ctx.Addresses.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var lodgings = new Repository<AccountModel>(ctx);

          var actual = await lodgings.SelectAsync();

          Assert.NotEmpty(actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);

          var actual = await profiles.SelectAsync();

          Assert.NotEmpty(actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var addresses = new Repository<AddressModel>(ctx);

          var actual = await addresses.SelectAsync();

          Assert.NotEmpty(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var lodgings = new Repository<AccountModel>(ctx);

          var actual = await lodgings.SelectAsync(1);

          Assert.NotNull(actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);

          var actual = await profiles.SelectAsync(1);

          Assert.NotNull(actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var addreses = new Repository<AddressModel>(ctx);

          var actual = await addreses.SelectAsync(1);

          Assert.NotNull(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_Update()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new AccountContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new AccountContext(_options))
        {
          var lodgings = new Repository<AccountModel>(ctx);
          var expected = await ctx.Accounts.FirstAsync();

          expected.Name = "name";
          lodgings.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Accounts.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);
          var expected = await ctx.Profiles.FirstAsync();

          expected.Email = "email";
          profiles.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Profiles.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var addreses = new Repository<AddressModel>(ctx);
          var expected = await ctx.Addresses.FirstAsync();

          expected.City = "Denver";
          addreses.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Addresses.FirstAsync();

          Assert.Equal(expected, actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
