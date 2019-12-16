using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.ArticleSystem.Business
{
	public abstract class BaseService
	{
		protected CorporateUnitOfWork _corporateRepository = null;
		protected IMemoryCache _cache = null;
		protected string _cacheKey = null;

		public BaseService(IServiceProvider serviceProvider, string cacheKey)
		{
			_corporateRepository = (CorporateUnitOfWork)serviceProvider.GetRequiredService( typeof( IUnitOfWork ) );
			_cache = (IMemoryCache)serviceProvider.GetRequiredService( typeof( IMemoryCache ) );
			_cacheKey = cacheKey;
		}
	}
}
