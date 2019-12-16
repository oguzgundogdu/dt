using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Corporate.ArticleSystem.DataAccess
{
	public interface IGenericRepository<T>
		where T : class
	{
		T FindById(object EntityId);
		IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null);
		void Insert(T Entity);
		void Update(T Entity);
		void Delete(object EntityId);
		void Delete(T Entity);
	}
}
