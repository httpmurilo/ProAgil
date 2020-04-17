using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProaAgil.Repository.Data;
using ProAgil.Domain.Profiles;
using ProAgil.Repository.Interface;
using ProAgil.Repository.Repository;

namespace ProaAgil.Application {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();
            services.AddCors ();
            services.AddMvc (options => options.EnableEndpointRouting = false);
            services.AddDbContext<DataContext> (x => x.UseSqlite (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddScoped<IRepositoryBase, RepositoryBase> ();
            services.AddAutoMapper (typeof (AutoMapperProfiles));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else 
            {
                app.UseExceptionHandler (appBuilder => {
                    appBuilder.Run (async context => {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync ("Vish, apresentamos um erro interno :( tente revisar os dados e tentar novamente");
                    });
                });
            }
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());
            app.UseStaticFiles ();
            app.UseStaticFiles (new StaticFileOptions () {
                FileProvider = new PhysicalFileProvider (Path.Combine (Directory.GetCurrentDirectory (), @"Resources")),
                    RequestPath = new PathString ("/Resources")
            });
            app.UseStaticFiles ();
            app.UseMvc ();
        }
    }
}