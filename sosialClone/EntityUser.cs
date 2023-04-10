

using System.ComponentModel.DataAnnotations.Schema;

namespace sosialClone
{
    public class EntityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ActivityId { get; set; }
        public Entities Activity { get; set; }
        public bool isHost { get; set; }
    }
}
