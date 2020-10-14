using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AccountContext _context;

    public virtual IRepository<AccountModel> Account { get; }
    public virtual IRepository<ProfileModel> Profile { get; }
    public virtual IRepository<AddressModel> Address { get; }
    public virtual IRepository<PaymentModel> Payment { get; }

    public UnitOfWork(AccountContext context)
    {
      _context = context;

      Account = new AccountRepository(context);
      Profile = new Repository<ProfileModel>(context);
      Address = new Repository<AddressModel>(context);
      Payment = new Repository<PaymentModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
