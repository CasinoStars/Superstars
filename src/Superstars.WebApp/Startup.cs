using System.Security.Claims;
using System.Text;
using CrashGameMath;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;

namespace Superstars.WebApp
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
            services.AddOptions();
            services.AddMvc();

            var secretKey = Configuration["JwtBearer:SigningKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            services.Configure<TokenProviderOptions>(o =>
            {
                o.Audience = Configuration["JwtBearer:Audience"];
                o.Issuer = Configuration["JwtBearer:Issuer"];
                o.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
            services.AddSignalR();
            services.AddSingleton(x => new SqlConnexion(Configuration["ConnectionStrings:SuperstarsDB"]));
            services.AddSingleton<UserGateway>();
            services.AddSingleton<GameGateway>();
            services.AddSingleton<YamsGateway>();
            services.AddSingleton<WalletGateway>();
            services.AddSingleton<RankGateway>();
            services.AddSingleton<BlackJackGateway>();
            services.AddSingleton<YamsService>();
            services.AddSingleton<ProvablyFairGateway>();
            services.AddSingleton<ChatGateway>();
            services.AddSingleton<CrashGateway>();

            services.AddSingleton(x => new BlackJackService());
            services.AddHostedService<CrashService>();
            services.AddSingleton(x => new RankService());
            services.AddSingleton<YamsIAService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton(x => new CrashBuilder(1000, "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526"));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy =>
                policy.RequireClaim(AllinClaimsNames.Role, "Admin"));
            });

            services.AddAuthentication(CookieAuthentication.AuthenticationScheme)
                .AddCookie(CookieAuthentication.AuthenticationScheme)
                .AddJwtBearer(JwtBearerAuthentication.AuthenticationScheme,
                    o =>
                    {
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = signingKey,

                            ValidateIssuer = true,
                            ValidIssuer = Configuration["JwtBearer:Issuer"],

                            ValidateAudience = true,
                            ValidAudience = Configuration["JwtBearer:Audience"],

                            NameClaimType = ClaimTypes.Name,
                            AuthenticationType = JwtBearerAuthentication.AuthenticationType
                        };
                    });
        }

        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            var secretKey = Configuration["JwtBearer:SigningKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRHub>("/SignalR");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller}/{action}/{id?}",
                    new {controller = "Home", action = "Index"});

                routes.MapRoute(
                    "spa-fallback",
                    "Home/{*anything}",
                    new {controller = "Home", action = "Index"});
            });

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from 2nd delegate.");
            //});
        }
    }
}