using PaparaApartment.Business.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaparaApartment.Core.DependencyInjection;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.IoC;
using PaparaApartment.Core.Utilities.Security;
using PaparaApartment.Core.Utilities.Security.Encryption;
using PaparaApartment.Core.Utilities.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace PaparaApartment.WebApi
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

            services.AddControllers();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApartmentManagement.WebApi", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:44394", "http://localhost:3000"));
            });

            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });


            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                });

            services.AddDependencyInjection(new ICoreModule[] { new CoreModule() });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApartmentManagement.WebApi v1"));
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
