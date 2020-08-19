using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.ObjectModel.Models;
namespace RVTR.Account.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Account_Repository_
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class AccountRepository : Repository<AccountModel>
  {

    public AccountRepository(AccountContext context) : base(context) { }

    public override async Task<AccountModel> SelectAsync(int id) => await _db
                                                                          .Where(x => x.Id == id)
                                                                          .Include(x => x.Address)
                                                                          .Include(x => x.Profiles)
                                                                          .Include(x => x.Payments)
                                                                          .FirstOrDefaultAsync();

    public override async Task<IEnumerable<AccountModel>> SelectAsync() => await _db
                                                                          .Include(x => x.Address)
                                                                          .Include(x => x.Profiles)
                                                                          .Include(x => x.Payments)
                                                                          .ToListAsync();
  }
}