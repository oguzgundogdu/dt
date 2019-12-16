using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Common.Security;
using Corporate.ArticleSystem.DataAccess;
using Corporate.ArticleSystem.Dto;
using Corporate.ArticleSystem.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corporate.ArticleSystem.Business
{
	public class UserService: BaseService, IUserService
	{
		public UserService(IServiceProvider serviceProvider) : base( serviceProvider, "users" )
		{

		}

		public void InsertUser(User user)
		{
			user.Password = Cryptography.ComputeSha256Hash( user.Password );
			_corporateRepository.UserRepository.Insert( user );
		}

		public void UpdateUser(User user)
		{
			if (!string.IsNullOrWhiteSpace( user.Password )) {
				user.Password = Cryptography.ComputeSha256Hash( user.Password );
			}
			else {
				User tempUser = _corporateRepository.UserRepository.FindById( user.UserId );
				user.Password = tempUser.Password;
			}

			_corporateRepository.UserRepository.Update( user );
		}

		public void DeleteUser(User user)
		{
			_corporateRepository.UserRepository.Delete( user );
		}

		public User GetUserById(int userId)
		{
			return _corporateRepository.UserRepository.FindById( userId );
		}

		public void SaveAllChanges()
		{
			_corporateRepository.Save();
		}
	}
}
