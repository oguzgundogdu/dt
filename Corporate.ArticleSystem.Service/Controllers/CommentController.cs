using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corporate.ArticleSystem.Entity;
using Corporate.CommentSystem.Business.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Corporate.ArticleSystem.Service.Controllers
{
	[Route( "[controller]" )]
	[ApiController]
	public class CommentController: ControllerBase
	{
		private readonly ILogger _logger;
		public CommentController(ILogger<CommentController> logger)
		{
			_logger = logger;
		}

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpGet]
		public IActionResult Get([FromServices] ICommentService commentService)
		{
			try {
				IEnumerable<Comment> comments = commentService.GetComments();

				return Ok( comments );
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
				return StatusCode( 500 );
			}
		}

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpGet( "{id}" )]
		public IActionResult GetById(int id, [FromServices] ICommentService commentService)
		{
			try {
				Comment comment = commentService.GetCommentById( id );

				return Ok( comment );
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
				return StatusCode( 500 );
			}
		}

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpPost]
		public IActionResult Add([FromBody]Comment comment, [FromServices] ICommentService commentService)
		{
			try {
				if (comment != null && comment.ArticleId <= 0) {
					commentService.InsertComment( comment );
					commentService.SaveAllChanges();
					return Ok( comment );
				}
				else {
					return UnprocessableEntity();
				}
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
				return StatusCode( 500 );
			}
		}
	}
}