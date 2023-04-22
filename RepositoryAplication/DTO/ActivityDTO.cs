

using RepositoryAplication.DTOs;

namespace RepositoryAplication.Activities
{
    public class ActivityDTO
    {
       //DTOs to send data  
        public Guid Id { get; set; }

        //[Required] I will use fluentValdation (automatic validation)
        public string Title { get; set; }

        // [Required]
        public DateTime Date { get; set; }

        //  [Required]
        public string Description { get; set; }

        // [Required]
        public string Catagory { get; set; }

        //  [Required]
        public string City { get; set; }

        //  [Required]
        public string Venue { get; set; }
        public string HostUsername { get; set; }

        public bool isCancled { get; set; }

        public ICollection<ProfileDTO> Attendies { get; set; }
    }
}
