using AspNetCoreSharedComponent.Middleware;
using DatabaseAccessor.Contexts;
using GUI.Abtractions;
using GUI.Attributes;
using GUI.Clients;
using GUI.Payments.Factory;
using GUI.Payments.Momo.Cryptography;
using GUI.Payments.Momo.Processor;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System;
using System.Net.Http;
using System.Reflection;

namespace GUI
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
            IdentityModelEventSource.ShowPII = true;
            services.AddControllersWithViews().AddSessionStateTempDataProvider();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["REDIS_CONNECTION_STRING"];
            });
            services.AddDataProtection()
                .PersistKeysToDbContext<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>();
            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                "https://cap-k24-team13-auth.herokuapp.com/.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever());
            var openIdConfig = configManager.GetConfigurationAsync().Result;
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = "/Error/403";
                options.ExpireTimeSpan = TimeSpan.FromHours(12);
                options.SlidingExpiration = false;
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://cap-k24-team13-auth.herokuapp.com";
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.MetadataAddress = "https://cap-k24-team13-auth.herokuapp.com/.well-known/openid-configuration";
                options.ClientId = "oidc-client";
                options.ClientSecret = "CapK24Team13";
                options.ResponseType = "code";
                options.UsePkce = true;
                options.ResponseMode = "query";
                options.Scope.Add("offline_access");
                options.Scope.Add("roles");
                options.Scope.Add("shop");
                options.Scope.Add("IdentityServerApi");
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("ShopId", "ShopId");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuers = new[]
                    {
                        "https://cap-k24-team13-auth.herokuapp.com/"
                    },
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeys = openIdConfig.SigningKeys,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = true
                };
            });
            services.Configure<RazorViewEngineOptions>(options =>
            {
                var virtualAreaName = typeof(BaseUserController)
                    .GetCustomAttribute<VirtualAreaAttribute>(false)
                    .Name;
                options.ViewLocationFormats.Add($"/Areas/{virtualAreaName}/Views/{{1}}/{{0}}{RazorViewEngine.ViewExtension}");
                options.ViewLocationFormats.Add($"/Areas/{virtualAreaName}/Views/Shared/{{0}}{RazorViewEngine.ViewExtension}");
            });
            services.AddScoped<BaseUserActionFilter>();
            services.AddScoped<IShopClient, ShopClientImpl>();
            services.AddScoped<HttpClient>();
            services.AddScoped(servicesProvider => new MomoWalletSecurity(Configuration["MOMO_SECRET_KEY"]));
            services.AddScoped<MomoWalletProcessor>();
            services.AddScoped<PaymentProcessorFactory>();
            services.AddRefitClient<IProductClient>()
                .ConfigureHttpClient(ConfigureHttpClient);
            services.AddRefitClient<IExternalShopClient>(new RefitSettings
            {
                ContentSerializer = new CustomRefitJsonContentSerializer()
            })
                .ConfigureHttpClient(options =>
                {
                    options.BaseAddress = new Uri("https://emallsolution-backendapi.herokuapp.com/api");
                });
            services.AddRefitClient<ICategoryClient>()
                .ConfigureHttpClient(ConfigureHttpClient);
            services.AddRefitClient<ICartClient>()
                .ConfigureHttpClient(ConfigureHttpClient);
            services.AddRefitClient<IInvoiceClient>()
                .ConfigureHttpClient(ConfigureHttpClient);
            services.AddRefitClient<IShopInterfaceClient>()
                .ConfigureHttpClient(ConfigureHttpClient);
            services.AddRefitClient<IUserClient>()
                .ConfigureHttpClient(ConfigureHttpClient);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(12);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto
            });
            app.UseHsts();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGlogalExceptionHandlerMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                        name: "ShopOwner",
                        areaName: "ShopOwner",
                        pattern: "ShopOwner/{controller=Product}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                        name: "Admin",
                        areaName: "Admin",
                        pattern: "Admin/{controller}/{action}/{id?}");

                endpoints.MapControllerRoute(
                        name: "User",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://ec2-13-215-160-187.ap-southeast-1.compute.amazonaws.com:3000");
        }
    }
}