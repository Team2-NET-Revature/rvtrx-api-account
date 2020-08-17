using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using RVTR.Account.ObjectModel.Models;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AccountRepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<AccountContext> _options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new AccountModel() { Id = 1, Name = "name" },
        new ProfileModel() { Id = 1, Email = "email",  AccountId = 1 },
      }
    };

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

          Assert.Empty(actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);

          var actual = await profiles.SelectAsync();

          Assert.Empty(actual);
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

          Assert.Null(actual);
        }

        using (var ctx = new AccountContext(_options))
        {
          var profiles = new Repository<ProfileModel>(ctx);

          var actual = await profiles.SelectAsync(1);

          Assert.Null(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }



    public AccountRepositoryTest()
    {
    }
  }
}
