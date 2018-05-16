﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace server
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
            services.AddMediatR();
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");

                    options.UseSqlServer(connectionString);
                });

            services.AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer => {
                bearer.Authority = "https://accounts.google.com/";
                bearer.RequireHttpsMetadata = false;
                bearer.Audience = "42472227382-lv313luvu3etp0ck6vnfv67jj06kilv0.apps.googleusercontent.com";
            });

            services
                .AddMvc()
                .AddFeatureFolders();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseWelcomePage();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
