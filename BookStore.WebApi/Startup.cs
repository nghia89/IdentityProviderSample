using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.WebApi.Repositories;
using BookStore.WebAPI;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.WebApi
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
            services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));
            services.AddDistributedMemoryCache();
            services.AddAuthentication(
                IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44331";
                    options.ApiName = "bookstore_apis";
                    options.ApiSecret = "supersecret";
                    options.EnableCaching = true;
                });
            services.AddDbContext<BookStoreDBContext>(context => { context.UseInMemoryDatabase("BookStoreDB"); });
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

            services.AddAuthorization(
                o => {
                    //o.AddPolicy("CanViewBook", p => p.RequireRole("Administrator"));
                    o.AddPolicy("CanViewBook", p => p.RequireClaim("scope", "bookstore_viewbook"));
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
