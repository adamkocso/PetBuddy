﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD
using PetBuddy.Models;
=======
>>>>>>> b8fdf36118d5d47c79b3b7e69ac7863f3004e234
using PetBuddy.Services;

namespace PetBuddy
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("ProductionConnection")));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(builder =>
                    builder.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddTransient<IPetService, PetService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager)
        {
            Admin.CreateAdmin(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
