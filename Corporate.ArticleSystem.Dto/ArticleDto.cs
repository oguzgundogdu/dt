using Corporate.ArticleSystem.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corporate.ArticleSystem.Dto
{
	[Table( "t_articles", Schema = Constants.SCHEMA_NAME )]
	public class ArticleDto
	{
		[Column( "article_id" )]
		[Key]
		public int ArticleId { get; set; }

		[Column( "article_title" )]
		public string ArticleTitle { get; set; }

		[Column( "article_content" )]
		public string ArticleContent { get; set; }

		[Column( "creation_time" )]
		public DateTime CreationTime { get; set; }

		[Column( "last_modified_time" )]
		public DateTime LastModifiedTime { get; set; }

		[Column( "created_by" )]
		public int UserId { get; set; }
	}
}
