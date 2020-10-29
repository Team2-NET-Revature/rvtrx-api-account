using System.Threading.Tasks;
using RVTR.Account.ObjectModel.Models;

namespace RVTR.Account.ObjectModel.Interfaces
{
  public interface IUnitOfWork
  {
    IAccountRepository Account { get; }
    IRepository<AddressModel> Address { get; }
    IRepository<PaymentModel> Payment { get; }
    IRepository<ProfileModel> Profile { get; }

    Task<int> CommitAsync();
  }
}
