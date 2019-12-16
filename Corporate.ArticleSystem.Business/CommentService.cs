using Corporate.ArticleSystem.Common;
using Corporate.ArticleSystem.Dto;
using Corporate.ArticleSystem.Entity;
using Corporate.CommentSystem.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Corporate.ArticleSystem.Business
{
	public class CommentService: BaseService, ICommentService
	{
		public CommentService(IServiceProvider serviceProvider) : base( serviceProvider, "comments" )
		{

		}
		public void DeleteComment(Comment comment)
		{
			_corporateRepository.CommentRepository.Delete( comment );
		}

		public Comment GetCommentById(int commentId)
		{
			return _corporateRepository.CommentRepository.FindById( commentId );
		}

		public IEnumerable<Comment> GetComments(Expression<Func<Comment, bool>> filter = null)
		{
			Expression<Func<CommentDto, bool>> targetFilter = null;

			if (filter != null) {
				targetFilter = Conversion.ChangeExpression<Comment, CommentDto>( filter );
			}

			IEnumerable<Comment> resultCollection = _corporateRepository.CommentRepository.Select( targetFilter ).Select<CommentDto, Comment>( x => x );

			return resultCollection;
		}

		public void InsertComment(Comment comment)
		{
			_corporateRepository.CommentRepository.Insert( comment );
		}

		public void SaveAllChanges()
		{
			_corporateRepository.Save();
		}
	}
}
