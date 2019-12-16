using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Common;
using Corporate.ArticleSystem.Common.Security;
using Corporate.ArticleSystem.Dto;
using Corporate.ArticleSystem.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Corporate.ArticleSystem.Business
{
	public class SecurityService: ISecurityService
	{
		private CorporateUnitOfWork _corporateRepository = null;
		private AppSettings _appSettings = null;
		private bool _disposed = false;



		public SecurityService(IServiceProvider serviceProvider, IOptions<AppSettings> appSettings)
		{
			_corporateRepository = (CorporateUnitOfWork)serviceProvider.GetRequiredService( typeof( IUnitOfWork ) );
			_appSettings = appSettings.Value;
		}

		public User AuthenticateUser(string username, string password)
		{
			string hashedPassword = Cryptography.ComputeSha256Hash( password );

			User user = _corporateRepository.UserRepository.Select( u => u.UserName.Equals( username ) && u.Password.Equals( hashedPassword ) ).FirstOrDefault();

			if (user == null)
				return null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes( _appSettings.Secret );
			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity( new Claim[]
				{
					new Claim(ClaimTypes.Name, user.UserId.ToString())
				} ),
				Expires = DateTime.UtcNow.AddDays( 7 ),
				SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature )
			};
			var token = tokenHandler.CreateToken( tokenDescriptor );
			user.Token = tokenHandler.WriteToken( token );

			user.Password = null;

			return user;
		}

		public User GetCurrentUser(string token)
		{
			return null;
		}
	}
}
