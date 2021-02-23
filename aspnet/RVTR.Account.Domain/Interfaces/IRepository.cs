using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RVTR.Account.Domain.Interfaces
{
  public interface IRepository<TEntity> where TEntity : AEntity
  {
    Task DeleteAsync(int id);
    Task InsertAsync(TEntity entry);
    Task<IEnumerable<TEntity>> SelectAsync();
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate);
    void Update(TEntity entry);
  }
}
