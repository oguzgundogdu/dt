﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Corporate.ArticleSystem.Common.Logging
{
	public class FileLogger: ILogger
	{
		private static readonly object _syncRoot = new object();
		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			var message = string.Format( "{0}: {1} - {2}", logLevel.ToString(), eventId.Id, formatter( state, exception ) );
			WriteMessageToFile( message );
		}

		private static void WriteMessageToFile(string message)
		{
			lock (_syncRoot) {
				const string filePath = "C:\\AspCoreFileLog.txt";
				using (var streamWriter = new StreamWriter( filePath, true )) {
					streamWriter.WriteLine( message );
					streamWriter.Close();
				}
			}
		}
	}
}
