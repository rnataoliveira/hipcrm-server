using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Refit;
using server.Facades.Google;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
      services.AddCors();
      services.AddMemoryCache();
      services.AddMiniProfiler(options =>
      {
        options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
        options.TrackConnectionOpenClose = true;
        options.ResultsAuthorize = request => { return true; };
        options.RouteBasePath = "/profiler";
      });

      services.AddMediatR();
      services.AddDbContext<ApplicationDbContext>(options =>
          {
            var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");

            options.UseSqlServer(connectionString);
          });

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(bearer =>
      {
        bearer.Authority = "https://accounts.google.com/";
        bearer.RequireHttpsMetadata = false;
        bearer.Audience = "42472227382-lv313luvu3etp0ck6vnfv67jj06kilv0.apps.googleusercontent.com";
      });

      services.AddTransient<ICalendarApi>(
        sp => RestService.For<ICalendarApi>("https://www.googleapis.com/calendar/v3",
        new RefitSettings {
          JsonSerializerSettings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
          }
        })
      );

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
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
      app.UseMiniProfiler();
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
