﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class logInDTo
    {
        //DTOS to receive data
        [Required]
        public string Email { get; set; }
      

        [Required]
        public string Password { get; set; }
    }
}
