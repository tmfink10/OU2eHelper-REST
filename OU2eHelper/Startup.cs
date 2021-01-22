using System;
using Blazored.Modal;
using BlazorStrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OU2eHelper.Services;

namespace OU2eHelper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredModal();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBootstrapCss();
            services.AddHttpClient<IBaseAbilityService, BaseAbilityService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44332/");
            });
            services.AddHttpClient<IBaseAttributeService, BaseAttributeService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44332/");
            });
            services.AddHttpClient<IBaseSkillService, BaseSkillService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44332/");
            });
            services.AddHttpClient<IBaseTrainingValueService, BaseTrainingValueService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44332/");
            });
            services.AddHttpClient<IPlayerCharacterService, PlayerCharacterService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44332/");
            });
            services.AddHttpClient<IPlayerAbilityService, PlayerAbilityService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44332/");
            });

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
