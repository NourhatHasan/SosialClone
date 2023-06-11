﻿using sosialClone;

namespace RepositoryAplication.DTOs
{
    public class ProfileDTO
    {
        //DTOs to send data  
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string Picture { get; set; }

        public string username { get; set; }
        public ICollection<Photo> photos { get; set; }

    }
}
