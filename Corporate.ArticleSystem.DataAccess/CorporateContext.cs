using Corporate.ArticleSystem.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Corporate.ArticleSystem.DataAccess
{
	public class CorporateContext: DbContext
	{
		public CorporateContext()
		{

		}
		public CorporateContext(DbContextOptions<CorporateContext> options) : base( options )
		{

		}

		public DbSet<ArticleDto> Articles { get; set; }

		public DbSet<UserDto> Users { get; set; }

		public DbSet<CommentDto> Comments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured) {
				IConfigurationRoot configuration = new ConfigurationBuilder()
				   .SetBasePath( Directory.GetCurrentDirectory() )
				   .AddJsonFile( "appsettings.json" )
				   .Build();
				var connectionString = configuration.GetConnectionString( "CorporateDatabaseConnection" );
				optionsBuilder.UseNpgsql( connectionString );
			}
		}
	}
}
