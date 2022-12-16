using Blog_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog_API.DTO
{
    public class Blog
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Tags { get; set; }

    }
}