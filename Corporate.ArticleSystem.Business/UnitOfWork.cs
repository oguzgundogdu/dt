using Corporate.ArticleSystem.DataAccess;
using Corporate.ArticleSystem.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Corporate.ArticleSystem.Business
{
	public class CorporateUnitOfWork
		: IUnitOfWork
	{
		private CorporateContext _context = new CorporateContext();
		private CorporateRepository<UserDto> _userRepository;
		private CorporateRepository<ArticleDto> _articleRepository;
		private CorporateRepository<CommentDto> _commentRepository;
		private bool _disposed = false;
		internal CorporateRepository<UserDto> UserRepository
		{
			get
			{
				if (_userRepository == null)
					_userRepository = new CorporateRepository<UserDto>( _context );
				return _userRepository;
			}
		}
		internal CorporateRepository<ArticleDto> ArticleRepository
		{
			get
			{
				if (_articleRepository == null)
					_articleRepository = new CorporateRepository<ArticleDto>( _context );
				return _articleRepository;
			}
		}

		internal CorporateRepository<CommentDto> CommentRepository
		{
			get
			{
				if (_commentRepository == null)
					_commentRepository = new CorporateRepository<CommentDto>( _context );
				return _commentRepository;
			}
		}
		public void Save()
		{
			using (TransactionScope tScope = new TransactionScope()) {
				_context.SaveChanges();
				tScope.Complete();
			}
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed) {
				if (disposing) {
					_context.Dispose();
				}
			}
			this._disposed = true;
		}
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}
	}
}
