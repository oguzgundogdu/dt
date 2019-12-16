using Corporate.ArticleSystem.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.ArticleSystem.Business.Interfaces
{
	public interface ISecurityService
	{
		User AuthenticateUser(string username, string password);
		User GetCurrentUser(string token);
	}
}
