using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.ArticleSystem.Common.Logging.Provider
{
	public class FileLoggerProvider: ILoggerProvider
	{
		public ILogger CreateLogger(string categoryName)
		{
			return new FileLogger();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
