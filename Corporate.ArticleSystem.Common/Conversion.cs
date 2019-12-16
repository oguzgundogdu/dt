using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Corporate.ArticleSystem.Common
{
	public class Conversion
	{
		public static Expression<Func<T1, bool>> ChangeExpression<T, T1>(Expression<Func<T, bool>> exp)
		{
			var objParam = Expression.Parameter( typeof( T1 ) );
			var call = Expression.Invoke( exp, Expression.Convert( objParam, typeof( T1 ) ) );
			var outputExpression = Expression.Lambda<Func<T1, bool>>( call, objParam );

			return outputExpression;
		}

		public static Expression<Func<T, bool>> FuncToExpression<T>(Func<T, bool> func)
		{
			return x => func( x );
		}
	}
}
