using API.Services;
using Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using sosialClone;
using System.Text;
using static SecretPro.security.sHostRequirement;

namespace API
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
           IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
               
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<AppUser>>();


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            //configuration of authentication with JWTbearer
            // authentication comes after we get a token we need to check if the token is valid
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => {

                    //what to validate
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        //accept just the token which is signed
                        ValidateIssuerSigningKey = true,
                        //the same signing key to decrypt the token 
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };

                    
                    //get the token from the querry string to use it in signalR
                    //authenticate to SignalR hub
                    opts.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                            {
                                context.Token = accessToken;

                            }
                            return Task.CompletedTask;
                        }
                    };




                });

                services.AddAuthorization(opts =>
                  {
                    opts.AddPolicy("IsHost", policy =>
                    {
                      policy.Requirements.Add(new IsHostRequirement());
                     });
                });

            services.AddScoped<TokenServices>();
            return services;
        }
    }
}
