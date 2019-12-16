using Corporate.ArticleSystem.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Corporate.ArticleSystem.Entity
{
	public class Article: EntityBase
	{
		[DataMember]
		public int ArticleId { get; set; }

		[DataMember]
		public string ArticleTitle { get; set; }

		[DataMember]
		public string ArticleContent { get; set; }

		[DataMember]
		public DateTime CreationTime { get; set; }

		[DataMember]
		public DateTime LastModifiedTime { get; set; }

		[DataMember]
		public int UserId { get; set; }

		public static implicit operator ArticleDto(Article article)
		{
			if (article == null)
				return null;

			return new ArticleDto {
				ArticleId = article.ArticleId,
				ArticleTitle = article.ArticleTitle,
				ArticleContent = article.ArticleContent,
				CreationTime = article.CreationTime,
				LastModifiedTime = article.LastModifiedTime,
				UserId = article.UserId
			};
		}

		public static implicit operator Article(ArticleDto article)
		{
			if (article == null)
				return null;

			return new Article {
				ArticleId = article.ArticleId,
				ArticleTitle = article.ArticleTitle,
				ArticleContent = article.ArticleContent,
				CreationTime = article.CreationTime,
				LastModifiedTime = article.LastModifiedTime,
				UserId = article.UserId
			};
		}
	}
}
