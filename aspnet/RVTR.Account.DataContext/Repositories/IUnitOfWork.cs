using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.DataContext.Repositories
{
  public interface IUnitOfWork
  {
    AccountRepository Account { get; }
    Repository<AddressModel> Address { get; }
    Repository<PaymentModel> Payment { get; }
    Repository<ProfileModel> Profile { get; }

    Task<int> CommitAsync();
  }
}
