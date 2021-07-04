using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Repositories;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Mappers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Passenger.Infrastructure.Settings;
using System.Text;

namespace Passenger.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IUserRepository,InMemoryUserRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Passenger.Api", Version = "v1" });
            });

            services.AddAuthorization( x => 
            x.AddPolicy("admin", p => p.RequireRole("admin"))
            );
            services.AddMemoryCache();
            services.AddOptions();

            var jwtSettings = Configuration.GetSection("jwt").Get<JwtSettings>();
            services.AddOptions<JwtSettings>().Bind(Configuration.GetSection("JwtSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
            // var builder = new ContainerBuilder();
            // builder.Populate(services);
            // builder.RegisterModule(new ContainerModule(Configuration));
            // builder.RegisterModule(new SettingsModule(Configuration));
            // ApplicationContainer = builder.Build();
            // //return new AutofacServiceProvider(ApplicationContainer);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.

            builder.RegisterModule(new ContainerModule(Configuration));
            builder.RegisterModule(new SettingsModule(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Passenger.Api v1"));
            }

            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();

            if(generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());

        }
    }
}
