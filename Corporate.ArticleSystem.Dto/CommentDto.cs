using Corporate.ArticleSystem.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Corporate.ArticleSystem.Dto
{
	[Table( "t_comments", Schema = Constants.SCHEMA_NAME )]
	public class CommentDto
	{
		[Column( "comment_id" )]
		[Key]
		public int CommentId { get; set; }

		[Column( "article_id" )]
		[ForeignKey( "t_comments_t_articles_fkey" )]
		public int ArticleId { get; set; }

		[Column( "comment_content" )]
		public string CommentContent { get; set; }

		[Column( "creation_time" )]
		public DateTime CreationTime { get; set; }

		[Column( "created_by" )]
		public int UserId { get; set; }
	}
}
