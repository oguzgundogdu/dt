using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Corporate.CommentSystem.Business.Interfaces
{
	public interface ICommentService: IServiceBase
	{
		void InsertComment(Comment comment);

		void DeleteComment(Comment comment);

		Comment GetCommentById(int commentId);

		IEnumerable<Comment> GetComments(Expression<Func<Comment, bool>> filter = null);
	}
}
