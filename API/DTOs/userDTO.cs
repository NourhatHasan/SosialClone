namespace API.DTOs
{
    //what the user get after logging in
    public class userDTO
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        
        public string Picture { get; set; }
        public string Tokens { get; set; }
    }
}
