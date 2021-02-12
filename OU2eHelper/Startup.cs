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
using Syncfusion.Blazor;

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
            var uri = "https://localhost:44332/";

            services.AddBlazoredModal();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBootstrapCss();
            services.AddSyncfusionBlazor();
            services.AddServerSideBlazor().AddHubOptions(o => { o.MaximumReceiveMessageSize = 10485761; });
            services.AddHttpClient<IBaseAbilityService, BaseAbilityService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IBaseAttributeService, BaseAttributeService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IBaseSkillService, BaseSkillService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IBaseTrainingValueService, BaseTrainingValueService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IPlayerCharacterService, PlayerCharacterService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IPlayerAbilityService, PlayerAbilityService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IPlayerSkillService, PlayerSkillService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });
            services.AddHttpClient<IPlayerAttributeService, PlayerAttributeService>(client =>
            {
                client.BaseAddress = new Uri(uri);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzk3MjM0QDMxMzgyZTM0MmUzMGxiTFliWVRHbTVua0VnZ3pVeE9hUWpJc2d0YUhKRE54U2VLelRtTkxPdWM9;Mzk3MjM1QDMxMzgyZTM0MmUzMFhJc1hUUmF6dHlZRk1vTm9zUFVYN0dUY09Od3dHcDhwWUFXbHZ1Q1ZHTjg9");
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
