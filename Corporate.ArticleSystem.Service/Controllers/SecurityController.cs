using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Corporate.ArticleSystem.Service.Controllers
{
	[Route( "[controller]" )]
	[ApiController]
	public class SecurityController: ControllerBase
	{
		[HttpPost]
		public IActionResult AuthenticateUser([FromServices]ISecurityService securityService, [FromBody]User user)
		{
			User currentUser = securityService.AuthenticateUser( user.UserName, user.Password );

			return Ok( currentUser );
		}
	}
}