using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AccountRepositoryTest : IDisposable
  {
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AccountContext> _options;
    private bool _disposedValue;

    public AccountRepositoryTest()
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
    public async void Test_Repository_SelectAsync()
    {
      using var ctx = new AccountContext(_options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync();

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using var ctx = new AccountContext(_options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync(1);

      Assert.NotNull(actual);
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
