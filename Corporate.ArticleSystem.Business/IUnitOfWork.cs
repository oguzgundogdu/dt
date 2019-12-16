using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.ArticleSystem.Business
{
	public interface IUnitOfWork
		: IDisposable
	{
		void Save();
	}
}
