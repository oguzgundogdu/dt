using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
	public class ArticleController: ControllerBase
	{
		private readonly ILogger _logger;

		public ArticleController(ILogger<ArticleController> logger)
		{
			_logger = logger;
		}

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpGet]
		public IActionResult Get([FromServices] IArticleService articleService)
		{
			try {
				IEnumerable<Article> articles = articleService.GetArticles();

				return Ok( articles );
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
				return StatusCode( 500 );
			}
		}

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpGet( "{id}" )]
		public IActionResult GetById(int id, [FromServices] IArticleService articleService)
		{
			try {
				Article article = articleService.GetArticleById( id );

				return Ok( article );
			}
			catch (Exception ex) {
				_logger.Log( LogLevel.Error, ex.Message );
				return StatusCode( 500 );
			}
		}

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpPost]
		public IActionResult Add([FromBody]Article article, [FromServices] IArticleService articleService)
		{
			try {
				if (article != null && article.ArticleId <= 0) {
					articleService.InsertArticle( article );
					articleService.SaveAllChanges();
					return Ok( article );
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

		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
		[HttpPut( "{id}" )]
		public IActionResult Update(int id, [FromBody]Article article, [FromServices] IArticleService articleService)
		{
			try {
				if (article != null && id > 0) {
					articleService.UpdateArticle( article );
					articleService.SaveAllChanges();
					return Ok( article );
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
