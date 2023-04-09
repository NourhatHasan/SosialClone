using API.Services;
using Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sosialClone;
using System.Text;

namespace API
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
           IConfiguration config)
        {
            services.AddIdentityCore<user>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>();


            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            //configuration of authentication with JWTbearer
            // authentication comes after we get a token we need to check if the token is valid
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>

                //what to validate
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    //accept just the token which is signed
                    ValidateIssuerSigningKey = true,
                    //the same signing key to decrypt the token 
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience=false

                }) ;


            services.AddScoped<TokenServices>();
            return services;
        }
    }
}
