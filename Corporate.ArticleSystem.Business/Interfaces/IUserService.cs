using Corporate.ArticleSystem.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.ArticleSystem.Business.Interfaces
{
	public interface IUserService: IServiceBase
	{
		void InsertUser(User user);


		void UpdateUser(User user);


		void DeleteUser(User user);


		User GetUserById(int userId);
	}
}
