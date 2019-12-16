using Corporate.ArticleSystem.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corporate.ArticleSystem.Dto
{
	[Table( "t_users", Schema = Constants.SCHEMA_NAME )]
	public class UserDto
	{
		[Column( "user_id" )]
		[Key]
		public int UserId { get; set; }

		[Column( "user_name" )]
		public string UserName { get; set; }

		[Column( "user_password" )]
		public string Password { get; set; }

	}
}
