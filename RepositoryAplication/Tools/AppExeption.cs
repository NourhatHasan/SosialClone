﻿

namespace RepositoryAplication.Tools
{
    public class AppExeption
    {
        public AppExeption(int statusCode,string message, string details=null)
        {
            this.statusCode = statusCode;   
            this.message = message;
            this.details = details;
        }

        public int statusCode { get; set; }
        public string message { get; set; }
        public string details { get; set; }
    }
}
