

using Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SecretPro.security
{
    public class sHostRequirement
    {
        public class IsHostRequirement:IAuthorizationRequirement
        {

        }
        public class IsHostReuirementHandler : AuthorizationHandler<IsHostRequirement>
        {
            private readonly IHttpContextAccessor httpContextAccessor;
            private readonly DataContext dataContext;

            public IsHostReuirementHandler(IHttpContextAccessor httpContextAccessor
                , DataContext dataContext)
            {
                this.httpContextAccessor = httpContextAccessor;
                this.dataContext = dataContext;
            }
            

            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
            {
                //var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId =context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                

                //we need to make the guid to string 
                var activityId = Guid.Parse(httpContextAccessor.HttpContext.Request.RouteValues.FirstOrDefault(x => x.Key == "Id").Value.ToString());

                
                
                // var atendy = dataContext.EntityUser.Find(userId, activityId);
               
                //to not missing the hostname
                var atendy = dataContext.EntityUser
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x=>x.AppUserId==userId
                                     && x.ActivityId== activityId).Result;

                //if the user is not host of the specific avtivity
                if (atendy == null ) { return Task.CompletedTask; }


                if (atendy.isHost)
                {
                  context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
        }
    }
}
