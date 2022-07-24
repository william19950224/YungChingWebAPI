using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YungChingWebAPI {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
			MapperConfig.Initialize();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddCors(options => {
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
			services.AddControllers().AddNewtonsoftJson();
			services.RegisterServices();
			services.AddSwaggerGen(options => {
				//show xml description
				var basePath = AppContext.BaseDirectory;
				options.IncludeXmlComments(System.IO.Path.Combine(basePath, "Api.xml"));
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
					Version = "v1",
					Title = "MEOW HTTP API",
					Description = "The MEOW Service HTTP API"
				});
			});
			services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options => {
				// �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
				options.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

				options.TokenValidationParameters = new TokenValidationParameters {
					// �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
					NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
					// �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
					RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

					// �@��ڭ̳��|���� Issuer
					ValidateIssuer = true,
					ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

					// �q�`���ӻݭn���� Audience
					ValidateAudience = false,
					//ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

					// �@��ڭ̳��|���� Token �����Ĵ���
					ValidateLifetime = true,

					// �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
					ValidateIssuerSigningKey = false,

					// "1234567890123456" ���ӱq IConfiguration ���o
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			app.UseCors("CorsPolicy");

			app.UseSwagger(c => {
				//Change the path of the end point , should also update UI middle ware for this change                
				c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
			});
			//http://localhost:44392/api-docs/swagger/index.html
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "MEOW v1");
				//Include virtual directory if site is configured so
				c.RoutePrefix = "api/swagger";
			});
			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
