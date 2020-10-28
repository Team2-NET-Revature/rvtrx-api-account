using RVTR.Account.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Account.ObjectModel.Interfaces
{
  public interface IAccountRepository : IRepository<AccountModel>
  {
    Task<AccountModel> SelectByEmailAsync(string email);
  }
}
