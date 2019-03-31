using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Substitute.Bot;
using Substitute.Business.Services;
using Substitute.Business.Services.Impl;
using Substitute.Domain;
using Substitute.Domain.Data;
using Substitute.Domain.DataStore;
using Substitute.Domain.DataStore.Impl;

namespace Substitute.Webpage
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
            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<PgContext>()
                    .BuildServiceProvider();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })

            .AddCookie(options =>
            {
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            })

            .AddDiscord(options =>
            {
                options.ClientId = Settings.DiscordId;
                options.ClientSecret = Settings.DiscordSecret;
                options.SaveTokens = true;
                options.Scope.Add("identify");
                options.Scope.Add("guilds");
            });

            services.AddMemoryCache();

            #region Register services
            #region Domain services
            services.AddScoped<IContext, PgContext>();
            services.AddSingleton<ISingletonContext, PgContext>();
            services.AddSingleton<ICache, InMemoryCache>();
            services.AddSingleton<IStorage, FileStorage>();
            services.AddSingleton<ISnowflake, Snowflake>();
            #endregion
            #region Business services
            services.AddScoped<IBotService, BotService>();
            services.AddScoped<IGuildService, GuildService>();
            services.AddScoped<IImageResponseService, ImageResponseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IDiscordBotRestService, DiscordBotRestService>();
            #endregion
            #region Bot
            services.AddSingleton<IDiscordBot, DiscordBot>();
            #endregion
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.ApplicationServices.GetService<IDiscordBot>().LoginAndStart();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

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
