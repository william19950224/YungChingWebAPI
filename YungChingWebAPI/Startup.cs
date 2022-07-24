using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
