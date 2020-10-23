using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;
using TravelApp.Authentication;
using TravelApp.Services;

namespace TravelApp
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
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var Uri = s.GetRequiredService<IConfiguration>()["ConnectionString"];
                return new MongoClient(Uri);
            });
            services.AddControllers();
            services.AddScoped<PlaceService>();
            services.AddScoped<BucketListService>();
            services.AddScoped<UserService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(option => {
            //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = Configuration["JWT:ValidAudience"],
            //            ValidateAudience = true,
            //            ValidAudience = Configuration["JWT:ValidIssuer"],
            //            ValidateLifetime = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
            //        };
            //    });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
