using System;
using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Interfaces;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    private readonly AccountContext _context;
    private bool _disposedValue;

    public IRepository<AccountModel> Account { get; }
    public IRepository<ProfileModel> Profile { get; }
    public IRepository<AddressModel> Address { get; }
    public IRepository<PaymentModel> Payment { get; }

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

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _context.Dispose();
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
