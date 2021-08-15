using MediaMicroservice.Data.AccountMock;
using MediaMicroservice.Data.Medias;
using MediaMicroservice.DBContexts;
using MediaMicroservice.Helpers;
using MediaMicroservice.Logger;
using MediaMicroservice.ServiceCalls;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MediaMicroservice
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
            services.AddDbContext<MediaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MediaMicroserviceDb")));

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }
            ).AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("MediaApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo()
                     {
                         Title = "MediaMicroservice API",
                         Version = "1.0",
                         Description = "This API gives you the ability to view all multimedia content related to " +
                         "items for sale, list one specific medium, add new media, " +
                         "update and delete media.",
                         Contact = new Microsoft.OpenApi.Models.OpenApiContact
                         {
                             Name = "Bojana Neskovic",
                             Email = "bojananeskovic007@gmail.com"
                         },
                         License = new Microsoft.OpenApi.Models.OpenApiLicense
                         {
                             Name = "FTN licence",
                             Url = new Uri("http://www.ftn.uns.ac.rs/")
                         }
                     });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IAccountMockRepository, AccountMockRepository>();
            services.AddScoped<IItemForSaleService, ItemForSaleService>();

            services.AddScoped<IAuthHelper, AuthHelper>();

            services.AddSingleton<ILoggerMockRepository, LoggerMockRepository>();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Please try later.");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/MediaApiSpecification/swagger.json", "MediaMicroservice API");
                setupAction.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
