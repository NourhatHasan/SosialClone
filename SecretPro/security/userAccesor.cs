
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RepositoryAplication.SecretInterfaces.security
{
    public class UserAccesor : IUserAccesor
    {
        private readonly IHttpContextAccessor accessor;

        //we want to get acces to user
        //and we can do it by HttpContext
        public UserAccesor(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public string getUserName()
        {
            return accessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
