using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MT.OnlineRestaurant.AccountManagement.Facebook;
using MT.OnlineRestaurant.AccountManagement.Helpers;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.BusinessLayer.Interfaces;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.Interfaces;
using MT.OnlineRestaurant.ValidateUserHandler;
using MT.OnlineRestuarant.DataLayer;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace MT.OnlineRestaurant.AccountManagement
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "AccountManager", Version = "1.0" });
                c.OperationFilter<HeaderFilter>();
            });




            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserDataAccess, UserDataAccess>();
            services.AddTransient<IJwtFactory, JwtFactory>();

            var secreatKey = Configuration.GetValue<string>("AppSettings:SecretKey");
            services.Configure<ApplicationString>(Configuration.GetSection("ApplicationString"));
            services.Configure<FacebookCredentials>(Configuration.GetSection("FacebookCredentials"));
            var jwtOptions = Configuration.GetSection("JwtOptions").Get<Token.JwtOptions>();
            jwtOptions.SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatKey)), SecurityAlgorithms.HmacSha256);
            services.AddSingleton(jwtOptions);


            services.AddMvc(
                          config =>
                          {
                              //config.Filters.Add(new LoggingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));
                              //config.Filters.Add(new ErrorHandlingFilter(Configuration.GetConnectionString("DatabaseConnectionString")));

                          });
            //addedd
            services.AddDbContext<CustomerManagementContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"),
                b => b.MigrationsAssembly("MT.OnlineRestuarant.DataLayer")));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<CustomerManagementContext>().AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateIssuerSigningKey = true
                 };

             })
             .AddFacebook(facebookOptions =>
             {
                 facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"]; ;
                 facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                 facebookOptions.SaveTokens = true;
                 facebookOptions.Scope.Add("email");
                 facebookOptions.Scope.Add("user_birthday");
                 facebookOptions.Scope.Add("user_gender");
                 facebookOptions.Scope.Add("user_posts");
                 facebookOptions.Scope.Add("manage_pages");
                 facebookOptions.Scope.Add("publish_pages");
             })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "AccountManager (V 1.0)");
            });
            app.UseMvc();

        }
    }
}
