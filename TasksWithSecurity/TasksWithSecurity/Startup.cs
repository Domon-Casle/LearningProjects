using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TasksWithSecurity.Models;
using TasksWithSecurity.Services;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System;
using TasksWithSecurity.Data;
using AspNetCore.Identity.DocumentDb;

namespace TasksWithSecurity
{
    public class Startup
    {
        /// <summary>
        /// Props
        /// </summary>
        public IConfiguration Configuration { get; }
        public DocumentClient documentClient { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            documentClient = new DocumentClient(
                Configuration.GetValue<Uri>("DocumentDbClient:EndpointUri"),
                Configuration.GetValue<string>("DocumentDbClient:AuthorizationKey"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDocumentClient>(documentClient);
            DataBaseDocumentClientSecurity.InitializeDocumentClient(documentClient);
            DataBaseDocumentTasks<Item>.InitializeDocumentClient(documentClient);

            services.AddIdentity<ApplicationUser, DocumentDbIdentityRole>()
                .AddDocumentDbStores(options =>
                {
                    options.Database = DataBaseDocumentClientSecurity.DataBaseId;
                    options.UserStoreDocumentCollection = DataBaseDocumentClientSecurity.UserCollectionId;
                    options.RoleStoreDocumentCollection = DataBaseDocumentClientSecurity.RoleCollectionId;
                })
                .AddDefaultTokenProviders();

            services.AddAuthentication();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
