

namespace sosialClone
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public AppUser Auther { get; set; }
        public Entities Activity { get; set; }

        //want to store the time in database as UTC time
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
