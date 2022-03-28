using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MLCCommonILibrary.System.Config;
using MLCServiceApi.Data;
using MLCServiceApi.Options;
 

namespace MLCServiceApi
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

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<ConnectionSetting>(Configuration.GetSection("ConnectionStrings"));
            
            services.AddSingleton<IConnectionSetting, ConnectionSetting>();

            services.AddSingleton<IApplicationSettings, ApplicationSettings>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<TokenDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<TokenUser, IdentityRole>().AddEntityFrameworkStores<TokenDBContext>().AddDefaultTokenProviders();

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MLC API", Version = "v1.0" });
            });

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());


  

            //services.AddAuthentication(option => {
            //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(option => {
            //    option.RequireHttpsMetadata = false;
            //    option.SaveToken = true;
            //    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidIssuer = "http://wali.com",
            //        ValidAudience = "http://wali.com",
            //        ClockSkew =  TimeSpan.Zero
            //    };
            //});

            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });



            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials().Build();
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(builder =>
                builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
                .AllowAnyHeader()
                .AllowAnyOrigin()
                );


            var swaggerOptions = new SwaggerOptions();

            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            //http://localhost/MLCServiceApi/swagger/index.html
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Desription);
            });

 
            //SeedDatabase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseCors("EnableCORS");

            //var tes = Task.Run(() => InitializeRoles(roleManager));

        }


        // Initialize some test roles. In the real world, these would be setup explicitly by a role manager
        private readonly string[] roles = new[] { "User", "Manager", "Administrator" };
        private async Task<string> InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                
                }
            }
            return "Done Adding role";
        }

    }
}
