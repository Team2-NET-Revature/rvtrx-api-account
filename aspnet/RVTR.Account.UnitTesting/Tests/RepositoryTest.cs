using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class RepositoryTest : IDisposable
  {
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AccountContext> _options;
    private readonly AccountModel _account = new AccountModel() { Id = 3 };
    private readonly ProfileModel _profile = new ProfileModel() { familyName = "FN", givenName = "GN", Id = 3, Email = "anemail@random.com", Phone = "123456789", Type = "" };
    private readonly AddressModel _address = new AddressModel() { Id = 3, AccountId = 3 };
    private bool _disposedValue;

    public RepositoryTest()
    {
      _connection = new SqliteConnection("Data Source=:memory:");
      _connection.Open();
      _options = new DbContextOptionsBuilder<AccountContext>()
        .UseSqlite(_connection)
        .Options;

      using var ctx = new AccountContext(_options);
      ctx.Database.EnsureCreated();
    }

    [Fact]
    public async void Test_Repository_DeleteAsync()
    {
      using (var ctx = new AccountContext(_options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        var profile = await ctx.Profiles.FirstAsync();
        await profiles.DeleteAsync(profile.Id);
        Assert.Equal(EntityState.Deleted, ctx.Entry(profile).State);
      }

      using (var ctx = new AccountContext(_options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        var address = await ctx.Addresses.FirstAsync();
        await addresses.DeleteAsync(address.Id);
        Assert.Equal(EntityState.Deleted, ctx.Entry(address).State);
      }

      using (var ctx = new AccountContext(_options))
      {
        var accounts = new Repository<AccountModel>(ctx);
        var account = await ctx.Accounts.FirstAsync();
        await accounts.DeleteAsync(account.Id);
        Assert.Equal(EntityState.Deleted, ctx.Entry(account).State);
      }
    }

    [Fact]
    public async void Test_Repository_InsertAsync()
    {
      using (var ctx = new AccountContext(_options))
      {
        var accounts = new Repository<AccountModel>(ctx);
        await accounts.InsertAsync(_account);
        Assert.Equal(EntityState.Added, ctx.Entry(_account).State);
      }

      using (var ctx = new AccountContext(_options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        await profiles.InsertAsync(_profile);
        Assert.Equal(EntityState.Added, ctx.Entry(_profile).State);
      }

      using (var ctx = new AccountContext(_options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        await addresses.InsertAsync(_address);
        Assert.Equal(EntityState.Added, ctx.Entry(_address).State);
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using (var ctx = new AccountContext(_options))
      {
        var accounts = new Repository<AccountModel>(ctx);

        var actual = await accounts.SelectAsync();

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

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using (var ctx = new AccountContext(_options))
      {
        var accounts = new Repository<AccountModel>(ctx);

        var actual = await accounts.SelectAsync(1);

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
        var addresses = new Repository<AddressModel>(ctx);

        var actual = await addresses.SelectAsync(1);

        Assert.NotNull(actual);
      }
    }

    [Fact]
    public async void Test_Repository_Update()
    {
      AccountModel account;
      ProfileModel profile;
      AddressModel address;

      using (var ctx = new AccountContext(_options))
      {
        var accounts = new Repository<AccountModel>(ctx);
        account = await ctx.Accounts.FirstAsync();

        account.Name = "name";
        accounts.Update(account);
        Assert.Equal(account.Name, (await ctx.Accounts.FindAsync(account.Id)).Name);
      }

      using (var ctx = new AccountContext(_options))
      {
        var profiles = new Repository<ProfileModel>(ctx);
        profile = await ctx.Profiles.FirstAsync();

        profile.Email = "email";
        profiles.Update(profile);
        Assert.Equal(profile.Email, (await ctx.Profiles.FindAsync(profile.Id)).Email);
      }

      using (var ctx = new AccountContext(_options))
      {
        var addresses = new Repository<AddressModel>(ctx);
        address = await ctx.Addresses.FirstAsync();

        address.City = "Denver";
        addresses.Update(address);
        Assert.Equal(address.City, (await ctx.Addresses.FindAsync(address.Id)).City);
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _connection.Dispose();
        }
        _disposedValue = true;
      }
    }

    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
