using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class UnitOfWorkTest : IDisposable
  {
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AccountContext> _options;
    private bool _disposedValue;

    public UnitOfWorkTest()
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
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new AccountContext(_options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Account);
      Assert.NotNull(unitOfWork.Profile);
      Assert.Equal(0, actual);
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
