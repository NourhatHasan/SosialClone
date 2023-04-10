
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RepositoryAplication.SecretInterfaces.security
{
    public class userAccesor : userInterface
    {
        private readonly IHttpContextAccessor accessor;

        //we want to get acces to user
        //and we can do it by HttpContext
        public userAccesor(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public string getUserName()
        {
            return accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
