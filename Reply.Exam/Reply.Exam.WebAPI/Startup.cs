using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reply.Exam.WebAPI.Configurations;
using Serilog;
using Serilog.Events;

namespace Reply.Exam.WebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel
				.Information()
				.WriteTo.RollingFile("Logs/log-{Date}.txt", LogEventLevel.Information)
				.CreateLogger();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddControllers();
			services.AddAutoMapper();
			services.AddSwaggerConfig();
			services.AddDbContext(Configuration);
			services.AddDIConfiguration(Configuration);

			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(c =>
			{
				c.AllowAnyHeader();
				c.AllowAnyMethod();
				c.AllowAnyOrigin();
			});

			app.UseExceptionHandler(config =>
			{
				config.Run(async context =>
				{
					context.Response.StatusCode = 500;
					context.Response.ContentType = "application/json";

					var error = context.Features.Get<IExceptionHandlerFeature>();
					if (error != null)
					{
						var ex = error.Error;

						await context.Response.WriteAsync(ex.ToString());
					}
				});
			});

			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseAuthentication();
			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(s =>
			{
				s.RoutePrefix = string.Empty;
				s.SwaggerEndpoint("./swagger/v1/swagger.json", "Reply.Exam.WebAPI API v1");
			});
		}
	}
}
