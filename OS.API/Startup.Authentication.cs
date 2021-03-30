using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.API
{
    public class AuthenticationInstaller
    {
        public AuthenticationInstaller(IServiceCollection services, IConfiguration Configuration)
        {
            services
            .AddAuthentication(options =>
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
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])
                    )
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey(Configuration["Cookie:Username"]))
                        {
                            context.Token = context.Request.Cookies[Configuration["Cookie:Username"]];
                        }

                        if (context.Request.Cookies.ContainsKey(Configuration["Cookie:AuthToken"]))
                        {
                            context.Token = context.Request.Cookies[Configuration["Cookie:Username"]];
                        }

                        if (context.Request.Cookies.ContainsKey(Configuration["Cookie:RefreshToken"]))
                        {
                            context.Token = context.Request.Cookies[Configuration["Cookie:RefreshToken"]];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
