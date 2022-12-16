namespace Blog_API.Models
{
    public class Blog_TagModel
    {
        public int Id { get; set; }



        public int BlogId { get; set; }
        public BlogModel? Blog { get; set; }

        public int TagId { get; set; }      
        public TagModel? Tag { get; set; }
        
    }
}
