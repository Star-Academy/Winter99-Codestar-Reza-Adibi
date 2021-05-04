using Libraries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_9.Models;
using Project_9.Services;
using System;
using System.Collections.Generic;

namespace Project_9 {
    public class Startup {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(CorsOptions());
            services.AddControllers();
            services.AddScoped<ElasticIndex, SearchIndex>();
            services.AddScoped<ISearchService, SearchService>();
        }

        private Action<CorsOptions> CorsOptions() {
            var uris = new List<string>();
            for (int i = 0; configuration["CorsOrigins:" + i] != null; i++) {
                uris.Add(configuration["CorsOrigins:" + i]);
            }
            return options => {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                    builder => {
                        builder.WithOrigins(uris.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                );
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
