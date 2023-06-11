

namespace RepositoryAplication.DTO
{
    public class CommentDTO
    {
        //we need to have Id since we will use it in creation of comments
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string userName { get; set; }
        public string DisplayName { get; set; }
        public string picture { get; set; }
      

     
       
    }
}
