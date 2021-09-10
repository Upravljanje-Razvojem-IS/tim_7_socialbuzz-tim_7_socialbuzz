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
using PurchaseMicroservice.Data.AccountMock;
using PurchaseMicroservice.Data.DeliveryMock;
using PurchaseMicroservice.Data.ItemForSaleMock;
using PurchaseMicroservice.Data.Purchases;
using PurchaseMicroservice.DBContexts;
using PurchaseMicroservice.Helpers;
using PurchaseMicroservice.Logger;

namespace PurchaseMicroservice
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PurchaseDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PurchaseMicroserviceDb")));

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }
            ).AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("PurchaseApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo()
                     {
                         Title = "PurchaseMicroservice API",
                         Version = "1.0",
                         Contact = new Microsoft.OpenApi.Models.OpenApiContact
                         {
                             Name = "Aleksandra Knezevic",
                             Email = "kaleksandra46@gmail.com"
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

            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IAccountMockRepository, AccountMockRepository>();
            services.AddScoped<IDeliveryMockRepository, DeliveryMockRepository>();
            services.AddScoped<IItemForSaleMockRepository, ItemForSaleMockRepository>();

            services.AddScoped<IAuth, Auth>();

            services.AddSingleton<ILoggerMockRepository, LoggerMockRepository>();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

       
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
                setupAction.SwaggerEndpoint("/swagger/PurchaseApiSpecification/swagger.json", "PurchaseMicroservice API");
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
