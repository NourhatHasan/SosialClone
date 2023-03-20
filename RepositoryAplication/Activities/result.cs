
namespace RepositoryAplication.Activities
{
    public class result<T>
    {
        public bool success { get; set; }
        public T data { get; set; }
        public string error { get; set; }
        public static result<T> isSucses(T data) => new result<T> { success = true, data = data };
        public static result<T> Failiere(string error)=> new result<T> { success=false,error = error };
    }
   
}
