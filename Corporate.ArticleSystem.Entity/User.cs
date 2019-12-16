using Corporate.ArticleSystem.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Corporate.ArticleSystem.Entity
{
	public class User: EntityBase
	{
		[DataMember]
		public int UserId { get; set; }

		[DataMember]
		public string UserName { get; set; }

		[DataMember]
		public string Password { get; set; }

		[DataMember]
		public string Token { get; set; }

		public static implicit operator UserDto(User user)
		{
			return new UserDto {
				UserId = user.UserId,
				Password = user.Password,
				UserName = user.UserName,

			};
		}

		public static implicit operator User(UserDto user)
		{
			return new User {
				UserId = user.UserId,
				Password = user.Password,
				UserName = user.UserName,

			};
		}
	}
}
