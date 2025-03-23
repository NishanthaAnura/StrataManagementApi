using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.DataAccess;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi;
public static class ServiceConfigurationExtensions
{
    public static WebApplicationBuilder ConfigureAppServices(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });


        var jwtKey = builder.Configuration["Jwt:Key"];

        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT Key is missing in configuration. Please set 'Jwt:Key' in appsettings.json or user secrets.");
        }

        var key = Encoding.UTF8.GetBytes(jwtKey);
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { error = "Unauthorized" });
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { error = "Forbidden" });
                    return context.Response.WriteAsync(result);
                }
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Admin)));

            options.AddPolicy("BuildingMember", policy =>policy.RequireAssertion(context =>
            context.User.IsInRole(UserRoles.Owner) ||
            context.User.IsInRole(UserRoles.Tenant)));
        });

        // Add DbContext with SQL Server
        var connectionString = builder.Configuration.GetConnectionString("StrataManagementDbContextConnection")
            ?? throw new InvalidOperationException("Connection string 'StrataManagementDbContextConnection' not found.");

        builder.Services.AddDbContext<StrataManagementDbContext>(options =>
            options.UseSqlServer(connectionString));

        //Add Identity 
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<StrataManagementDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMemoryCache();

        builder.Services.AddScoped<IRepositery<Building>, BuildingRepository>();
        builder.Services.AddScoped<IBuildingService, BuildingService>();

        builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
        builder.Services.AddScoped<IOwnerService, OwnerService>();

        builder.Services.AddScoped<ITenantRepositery, TenantRepository>();
        builder.Services.AddScoped<ITenantService, TenantService>();

        builder.Services.AddScoped<IMaintenanceRequestRepository, MaintenanceRequestRepository>();
        builder.Services.AddScoped<IMaintenanceRequestService, MaintenanceRequestService>();

        return builder;
    }

    // method to seed the roles and admin user
    public static async Task SeedRolesAndAdminUser(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Seed roles
        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }
        if (!await roleManager.RoleExistsAsync(UserRoles.Owner))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Owner));
        }
        if (!await roleManager.RoleExistsAsync(UserRoles.Tenant))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Tenant));
        }

        // Seed admin user
        var adminUser = new ApplicationUser
        {
            UserName = "adminak@mbcm.com",
            Email = "adminak@mbcm.com",
            FullName = "Admin User"
        };
        var result = await userManager.CreateAsync(adminUser, "Adminak@987");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
        }


    }

}
