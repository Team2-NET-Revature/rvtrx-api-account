using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Account.Domain;
using RVTR.Account.Domain.Interfaces;

namespace RVTR.Account.Context.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : AEntity
  {
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public Repository(AccountContext context)
    {
      _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(int id) => _dbSet.Remove(await SelectAsync(e => e.EntityId == id));

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public virtual async Task InsertAsync(TEntity entry) => await _dbSet.AddAsync(entry).ConfigureAwait(true);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await _dbSet.ToListAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate)
    {
      var entity = await _dbSet.FirstOrDefaultAsync(predicate).ConfigureAwait(true);

      foreach (var navigation in _dbSet.Attach(entity).Navigations)
      {
        await navigation.LoadAsync();
      }

      return entity;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    public virtual void Update(TEntity entry) => _dbSet.Update(entry);
  }
}
