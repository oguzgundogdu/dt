using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate.ArticleSystem.Business;
using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Corporate.ArticleSystem.Service.Controllers
{
	[Route( "[controller]" )]
	[ApiController]
	public class UserController: ControllerBase
	{
		private readonly ILogger _logger;
		public UserController(ILogger<UserController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public void Save([FromServices]IUserService userService, [FromBody] User user)
		{
			try {
				if (user != null) {
					if (user.UserId <= 0) {

						userService.InsertUser( user );
						userService.SaveAllChanges();
					}

				}
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
			}
			finally {

			}
		}


		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpPut( "{id}" )]
		public void Put(int id, [FromBody] User user, [FromServices]IUserService userService)
		{
			try {
				if (user != null) {
					if (id > 0) {

						userService.UpdateUser( user );
						userService.SaveAllChanges();
					}

				}
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
			}
			finally {

			}
		}

	}
}
