using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext.Repositories
{
  public interface IUnitOfWork
  {
    IRepository<AccountModel> Account { get; }
    IRepository<AddressModel> Address { get; }
    IRepository<PaymentModel> Payment { get; }
    IRepository<ProfileModel> Profile { get; }

    Task<int> CommitAsync();
  }
}
