using RVTR.Account.DataContext;
using RVTR.Account.DataContext.Repositories;
using Xunit;

namespace RVTR.Account.UnitTesting.Tests
{
  public class AccountRepositoryTest : DataTest
  {
    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync();

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using var ctx = new AccountContext(Options);

      var accounts = new AccountRepository(ctx);

      var actual = await accounts.SelectAsync(1);

      Assert.NotNull(actual);
    }
  }
}
