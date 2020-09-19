using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectTracking.AppStart;
using ProjectTracking.Data;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Services;
using ProjectTracking.Hubs;
using System;
using ProjectTracking.DataContract;
using System.Collections.Generic;
using ProjectTracking.Data.Methods;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectTracking.Data.Methods.Interfaces.Statistics;
using Microsoft.AspNetCore.HttpOverrides;
using ProjectTracking.Data.DataAccess;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProjectTracking.Managers;

namespace ProjectTracking
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
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AutomaticAuthentication = false;
            //});

            services.AddSingleton<IConfiguration>(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllUsersCanAccess", policy =>
                       policy.RequireRole("Manager", "Admin", "ApplicationUser"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administration", policy =>
                       policy.RequireRole("Manager", "Admin"));
            });

            Setting.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"));
                }, ServiceLifetime.Transient);

            //options.UseLazyLoadingProxies();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            //mapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IProjectsMethods, Data.Methods.Projects>();

            services.AddScoped<IProjectFilesMethods, Data.Methods.ProjectFilesMethods>();

            services.AddScoped<IProjectsStatistics, Data.Methods.Statistics.ProjectsProgresses>();


            services.AddScoped<IMeasurementUnitsMethods, Data.Methods.MeasurementUnitsMethods>();
            services.AddScoped<ITimeSheetActivityLogsMethods, Data.Methods.TimeSheetActivityLogsMethods>();
            services.AddScoped<IInsightsMethods, Data.Methods.Statistics.InsightsMethods>();

            //services.AddScoped<IInventoryProjectsMethods, Data.Methods.InventoryProjectsMethods>();
            //services.AddScoped<IPermissionsMethods, Data.Methods.Permissions>();
            //services.AddScoped<IRequestedPermissions, RequestedPermissionsMethods>();
            //services.AddScoped<IInventoryTypesMethods, Data.Methods.InventoryTypesMethods>();
            //services.AddScoped<IInventoryStatusesMethods, Data.Methods.InventoryStatusesMethods>();
            //services.AddScoped<ICountriesMethods, Data.Methods.CountriesMethods>();
            //services.AddScoped<IUpdateFrequenciesMethods, Data.Methods.UpdateFrequenciesMethods>();
            //services.AddScoped<IPublishingChannelsMethods, Data.Methods.PublishingChannelsMethods>();
            //services.AddScoped<ISubProjectsMethods, Data.Methods.SubProjectsMethods>();
            //services.AddScoped<IHolidaysMethods, HolidaysMethods>();
            //services.AddScoped<INotificationsHub, NotificationsHub>();

            services.AddScoped<ITimeSheetActivitiesMethods, Data.Methods.TimeSheetActivitiesMethods>();
            services.AddScoped<ITypeOfWorkMethods, Data.Methods.TypeOfWorkMethods>();
            services.AddScoped<IIpAddressMethods, Data.Methods.IpAddressesMethods>();

            services.AddTransient<INotificationMethods, Data.Methods.NotificationMethods>();
            services.AddScoped<ITimeSheetsMethods, Data.Methods.TimeSheets>();
            services.AddTransient<ICategoriesMethods, Data.Methods.CategoriesMethods>();
            services.AddTransient<ITeamsMethods, Data.Methods.TeamsMethods>();
            services.AddTransient<IUserMethods, Data.Methods.Users>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register identity roles 
            services.AddIdentity<ApplicationUser, IdentityRole>()
                         .AddEntityFrameworkStores<ApplicationDbContext>()
                         .AddDefaultTokenProviders()
                         .AddDefaultUI();


            #region Jwt Handlers and Managers

            //JwtConfiguration jwtConfiguration = Configuration.GetSection("Jwt").Get<JwtConfiguration>();
            //JwtManager jwtManager = new JwtManager(jwtConfiguration);

            //services.AddAuthentication(sharedOptions =>
            //{
            //    sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    sharedOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            //    // sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //}).AddJwtBearer(
            //    options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.SaveToken = true;

            //        options.TokenValidationParameters = jwtManager.GetTokenValidationParameters();
            //    });

            //// Add custom authorization handlers
            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            //    config.AddPolicy(Policies.User, Policies.UserPolicy());
            //});

            //services.AddSingleton<IJwtManager>(k => jwtManager);

            ////services.AddCors();

            //services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            //{
            //    builder.WithOrigins("http://localhost:3000")
            //           .AllowAnyMethod()
            //           .AllowAnyHeader();
            //}));
            ////services.AddSingleton<IAuthorizationHandler, JwtAuthorizationHandler>();

            #endregion

            services.AddSignalR();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IDataAccess, DataAccess>(k => new DataAccess(Setting.ConnectionString));

            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddHostedService<LiveObserverHost>();

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opt =>
    {
        //configure your other properties
        opt.LoginPath = "/login";
    });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //.AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, UserManager<ApplicationUser> userManager)
        {



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //lifetime


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseCors("AllowOrigin");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ObserverHub>("/observer");
                routes.MapHub<NotificationsHub>("/notificationshub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            //ApplicationDbInitializer.SeedUsers(userManager);
        }
    }
}
