﻿using AuthenticationApi.Infrastructure;
using BorrowMeAuth.Areas.Identity.Data;
using BorrowMeAuth.Data;
using Core.Repositories;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Domain.Entieties;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance;
using Persistance.Repositories;
using Services.Implementations;

namespace AuthenticationApi
{
    public class ConfigureStartup
    {
        public static void ConfigureIdentity(IServiceCollection services)
        {
            var b = services.AddIdentityCore<BorrowMeAuthUser>(q => q.User.RequireUniqueEmail = true);
            b = new IdentityBuilder(b.UserType, typeof(IdentityRole), services);
            b.AddRoles<IdentityRole>();
            b.AddEntityFrameworkStores<BorrowMeAuthContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }});
                    });
        }
        public static void AddAuthentication(WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = builder.Configuration.GetSection("Jwt").GetSection("Key").Value;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            ( path.StartsWithSegments("/chat") ))
                        {
                            context.Token = accessToken;
                        }
                        else
                        {
                            context.Token = context.Request.Cookies["jwt"];
                        }
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    ValidAudience = jwtSettings.GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
                };
            });

        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<DataDbContext, DataDbContext>();
            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
        }
    }

}
