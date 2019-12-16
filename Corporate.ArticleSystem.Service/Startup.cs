using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corporate.ArticleSystem.Business;
using Corporate.ArticleSystem.Business.Interfaces;
using Corporate.ArticleSystem.Common;
using Corporate.ArticleSystem.Common.Logging.Provider;
using Corporate.ArticleSystem.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Corporate.ArticleSystem.Service
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
			services.AddEntityFrameworkNpgsql().AddDbContext<CorporateContext>( opt =>
				 opt.UseNpgsql( Configuration.GetConnectionString( "CorporateDatabaseConnection" ) )
			);
			services.AddMemoryCache();

			var appSettingsSection = Configuration.GetSection( "AppSettings" );
			services.Configure<AppSettings>( appSettingsSection );

			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes( appSettings.Secret );

			services.AddAuthentication( x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			} )
			 .AddJwtBearer( x =>
			 {
				 x.RequireHttpsMetadata = false;
				 x.SaveToken = true;
				 x.Validate( JwtBearerDefaults.AuthenticationScheme );
				 x.TokenValidationParameters = new TokenValidationParameters {
					 ValidateIssuerSigningKey = true,
					 IssuerSigningKey = new SymmetricSecurityKey( key ),
					 ValidateIssuer = false,
					 ValidateAudience = false

				 };
				 x.Events = new JwtBearerEvents() {
					 OnTokenValidated = (context) =>
					 {
						 var name = context.Principal.Identity.Name;
						 if (string.IsNullOrEmpty( name )) { context.Fail( "Unauthorized. Please re-login" ); }
						 return Task.CompletedTask;
					 }
				 };
			 } );





			services.AddScoped<IUnitOfWork, CorporateUnitOfWork>();
			services.AddScoped<ISecurityService, SecurityService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IArticleService, ArticleService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddFile( "Logs/mylog-{Date}.txt" );

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();


		}
	}
}
