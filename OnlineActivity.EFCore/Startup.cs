using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineActivity.EFCore.Domain;
using OnlineActivity.EFCore.Infra;
using OnlineActivity.EFCore.WebApi.Utils;
using Swashbuckle.AspNetCore.Swagger;
using System.Data.SqlClient;

namespace OnlineActivity.EFCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var onlineStoreSettings = Configuration.GetSection("OnlineActivitySettings").Get<OnlineActivitySettings>();

            services.AddDbContext<OnlineActivityDbContext>(options =>
            {
                var connectionString = Configuration["OnlineActivitySettings:ConnectionString"];
                var password = Configuration["DbPassword"];
                var builder = new SqlConnectionStringBuilder(connectionString);

                var connection = builder.ConnectionString;
                options.UseSqlServer(connection);
            });

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info { Title = "OnlineActivity API", Version = "v1"});
            });

            services.AddCors(config =>
            {
                config.AddPolicy("OnlineActivityAngular6", policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                    policy.AllowCredentials();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseCors("OnlineActivityAngular6");

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "onlineactivity api v1");
            });

            app.UseMvc();
        }
    }
}
