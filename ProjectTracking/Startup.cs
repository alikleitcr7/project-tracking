﻿using System;
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
using ProjectTracking.DataContract;
using System.Collections.Generic;
using ProjectTracking.Data.Methods;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using ProjectTracking.Data.DataAccess;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using ProjectTracking.Utils;
//using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ProjectTracking
{
    public static class AuthPolicies
    {
        public const string Managers = "Managers";
        public const string TeamMembers = "TeamMembers";
        public const string Admins = "Admins";
        public const string Supervisors = "Supervisors";
    }
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

            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            services.AddMvc()
              .AddJsonOptions(options =>
              {
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              });


            services.AddSingleton<IConfiguration>(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AllUsersCanAccess", policy =>
            //           policy.RequireRole("Manager", "Admin", "ApplicationUser"));
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Administration", policy =>
            //           policy.RequireRole("Manager", "Admin"));
            //});

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
            });


            // policies

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.Managers, policy =>
                       policy.RequireRole(ApplicationUserRole.Admin.ToString(), ApplicationUserRole.Supervisor.ToString()));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.Admins, policy =>
                       policy.RequireRole(ApplicationUserRole.Admin.ToString()));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.TeamMembers, policy =>
                       policy.RequireRole(ApplicationUserRole.TeamMember.ToString()));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.Supervisors, policy =>
                       policy.RequireRole(ApplicationUserRole.Supervisor.ToString()));
            });


            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SupervisingPolicy", policy =>
            //        policy.Requirements.Add(new SupervisingPolicy(false)));
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("SupervisingOrAdminPolicy", policy =>
            //        policy.Requirements.Add(new SupervisingPolicy(true)));
            //});


            //services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, SupervisingPolicyHandler>();

            Setting.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"));
                }, ServiceLifetime.Scoped);


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

            services.AddScoped<IProjectsMethods, Data.Methods.ProjectsMethods>();
            services.AddScoped<ITasksMethods, Data.Methods.TasksMethods>();
            services.AddScoped<IUserLogsMethods, Data.Methods.UserLogsMethods>();
            services.AddScoped<ITimeSheetActivityLogsMethods, Data.Methods.TimeSheetActivityLogsMethods>();
            services.AddScoped<ITimeSheetActivitiesMethods, Data.Methods.TimeSheetActivitiesMethods>();
            services.AddScoped<IIpAddressMethods, Data.Methods.IpAddressesMethods>();
            services.AddScoped<IRoleKeyMethods, Data.Methods.RoleKeyMethods>();

            services.AddSingleton<Utils.IValidationExtensions, Utils.ValidationExtensions>();

            services.AddTransient<IBroadcastsMethods, Data.Methods.BroadcastsMethods>();
            services.AddTransient<INotificationMethods, Data.Methods.NotificationMethods>();

            services.AddScoped<ITimeSheetsMethods, Data.Methods.TimeSheetsMethods>();
            services.AddTransient<ICategoriesMethods, Data.Methods.CategoriesMethods>();
            services.AddTransient<ITeamsMethods, Data.Methods.TeamsMethods>();
            services.AddTransient<IUserMethods, Data.Methods.UsersMethods>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register identity roles 
            services.AddDefaultIdentity<ApplicationUser>()
                         .AddEntityFrameworkStores<ApplicationDbContext>()
                         .AddDefaultTokenProviders()
                         .AddDefaultUI();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsIdentityFactory>();

            //.AddSignInManager<SignInManager<ApplicationUser>>()
            //.AddRoleManager<RoleManager<ApplicationIdentityRole>>()

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

            services.AddSignalR(e => {
                e.EnableDetailedErrors = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSingleton<IDataAccess, DataAccess>(k => new DataAccess(Setting.ConnectionString));

            //services.AddTransient<UserManager<ApplicationUser>>();

            //services.AddHostedService<LiveObserverHost>();
            //services.AddSingleton<LiveObserverHost>();
            //services.AddHostedService<BackgroundServiceStarter<LiveObserverHost>>();

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opt =>
    {
        //configure your other properties
        opt.LoginPath = "/login";
        opt.AccessDeniedPath = new PathString("/accessdenied");

    });
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            //    .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //.AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseStatusCodePages();
                app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
            }
            else
            {
                app.UseExceptionHandler("/error");
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
                //routes.MapHub<BroadcastsHub>("/broadcastshub");
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
