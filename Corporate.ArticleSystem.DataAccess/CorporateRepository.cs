using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Corporate.ArticleSystem.DataAccess
{
	public class CorporateRepository<T>
		: IGenericRepository<T>
		where T : class
	{
		private CorporateContext _context;
		private DbSet<T> _dbSet;
		public CorporateRepository(CorporateContext Context)
		{
			_context = Context;
			_dbSet = _context.Set<T>();
		}
		public virtual T FindById(object EntityId)
		{
			return _dbSet.Find( EntityId );
		}
		public virtual IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null)
		{
			if (Filter != null) {
				return _dbSet.Where( Filter );
			}
			return _dbSet;
		}
		public virtual void Insert(T entity)
		{
			_dbSet.Add( entity );
		}
		public virtual void Update(T entityToUpdate)
		{
			_dbSet.Attach( entityToUpdate );
			_context.Entry( entityToUpdate ).State = EntityState.Modified;
		}
		public virtual void Delete(object EntityId)
		{
			T entityToDelete = _dbSet.Find( EntityId );
			Delete( entityToDelete );
		}
		public virtual void Delete(T Entity)
		{
			if (_context.Entry( Entity ).State == EntityState.Detached) {
				_dbSet.Attach( Entity );
			}
			_dbSet.Remove( Entity );
		}
	}
}
