using Corporate.ArticleSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Corporate.ArticleSystem.Business.Interfaces
{
	public interface IArticleService: IServiceBase
	{
		void InsertArticle(Article user);


		void UpdateArticle(Article user);


		void DeleteArticle(Article user);


		Article GetArticleById(int articleId);

		IEnumerable<Article> GetArticles(Func<Article, bool> filter = null);
	}
}
