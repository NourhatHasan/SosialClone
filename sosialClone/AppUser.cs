﻿

using Microsoft.AspNetCore.Identity;

namespace sosialClone
{
    public class AppUser: IdentityUser
    {
        //here we get all the properties
        //from IdentityUserClass
        //in addition to the one we add here
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        
        public ICollection<EntityUser> activities { get; set; }
        public ICollection<Photo> photos { get; set; }
        
        //who is following the current user
        public ICollection<Following> follwers { get; set; }
        
        //who the current user is following
        public ICollection<Following> followings { get; set; }
       
    }
}
