namespace Blog_API.DTO
{
    public class CreateBlog
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string[] Tags { get; set; }
    }
}
