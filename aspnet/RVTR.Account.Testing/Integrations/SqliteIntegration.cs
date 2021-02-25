using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Context;

namespace RVTR.Account.Testing
{
  public abstract class SqliteIntegration
  {
    private readonly SqliteConnection _connection;
    protected readonly DbContextOptions<AccountContext> options;

    public SqliteIntegration()
    {
      _connection = new SqliteConnection("Data Source=:memory:");
      options = new DbContextOptionsBuilder<AccountContext>().UseSqlite(_connection).Options;

      _connection.Open();

      using(var ctx = new AccountContext(options))
      {
        ctx.Database.EnsureCreated();
      }
    }
  }
}
