using Corporate.ArticleSystem.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Corporate.ArticleSystem.Entity
{
	public class Comment: EntityBase
	{
		[DataMember]
		public int CommentId { get; set; }

		[DataMember]
		public int ArticleId { get; set; }

		[DataMember]
		public string CommentContent { get; set; }

		[DataMember]
		public DateTime CreationTime { get; set; }

		[DataMember]
		public int UserId { get; set; }

		public static implicit operator Comment(CommentDto comment)
		{
			return new Comment {
				CommentId = comment.CommentId,
				ArticleId = comment.ArticleId,
				CommentContent = comment.CommentContent,
				CreationTime = comment.CreationTime,
				UserId = comment.UserId

			};
		}

		public static implicit operator CommentDto(Comment comment)
		{
			return new CommentDto {
				CommentId = comment.CommentId,
				ArticleId = comment.ArticleId,
				CommentContent = comment.CommentContent,
				CreationTime = comment.CreationTime,
				UserId = comment.UserId

			};
		}
	}
}
