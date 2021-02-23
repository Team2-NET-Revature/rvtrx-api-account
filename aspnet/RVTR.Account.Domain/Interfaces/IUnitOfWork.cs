using System.Threading.Tasks;
using RVTR.Account.Domain.Models;

namespace RVTR.Account.Domain.Interfaces
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
