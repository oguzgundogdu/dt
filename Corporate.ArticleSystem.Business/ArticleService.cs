using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Common;
using Corporate.ArticleSystem.Dto;
using Corporate.ArticleSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Corporate.ArticleSystem.Business
{
	public class ArticleService: BaseService, IArticleService
	{
		public ArticleService(IServiceProvider serviceProvider) : base( serviceProvider, "articles" )
		{

		}

		public void DeleteArticle(Article article)
		{
			IEnumerable<CommentDto> comments = _corporateRepository.CommentRepository.Select( x => x.ArticleId == article.ArticleId );

			foreach (CommentDto comment in comments) {
				_corporateRepository.CommentRepository.Delete( comment );
			}

			_corporateRepository.ArticleRepository.Delete( article );
		}

		public Article GetArticleById(int articleId)
		{
			return _corporateRepository.ArticleRepository.FindById( articleId );
		}

		public void InsertArticle(Article article)
		{
			_cache.Remove( _cacheKey );
			article.LastModifiedTime = article.CreationTime = DateTime.Now;
			_corporateRepository.ArticleRepository.Insert( article );
		}

		public void UpdateArticle(Article article)
		{
			_cache.Remove( _cacheKey );
			article.LastModifiedTime = DateTime.Now;
			_corporateRepository.ArticleRepository.Update( article );
		}

		public IEnumerable<Article> GetArticles(Func<Article, bool> filter = null)
		{
			object objArticlesCache;
			IEnumerable<ArticleDto> sourceCollection = null;

			if (_cache != null && _cache.TryGetValue( _cacheKey, out objArticlesCache ) && objArticlesCache != null) {
				sourceCollection = (IEnumerable<ArticleDto>)objArticlesCache;
			}

			IEnumerable<Article> resultCollection;

			if (sourceCollection == null)
				resultCollection = _corporateRepository.ArticleRepository.Select( filter == null ? null : Conversion.ChangeExpression<Article, ArticleDto>( Conversion.FuncToExpression( filter ) ) ).Select<ArticleDto, Article>( x => x );
			else
				resultCollection = sourceCollection.Where( x => filter( x ) ).Select<ArticleDto, Article>( x => x );

			return resultCollection;
		}

		public void SaveAllChanges()
		{
			_corporateRepository.Save();
		}
	}
}
