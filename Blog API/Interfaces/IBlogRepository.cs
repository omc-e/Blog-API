
using Blog_API.DTO;
using Blog_API.Models;

namespace Blog_API.Interfaces
{
    public interface IBlogRepository
    {
        List<Blog> GetBlogs();
        Blog GetBlog(string slug);
        bool BlogExists(string slug);
        List<CommentModel> GetComment(string slug);
        List<Blog> GetBlogFilteredByTag(string tagName);
        public bool DeleteBlog(BlogModel blog);
        bool TagExist(string tagName);
        string GenerateSlug(string title);
        public bool CreateComment(CommentModel comment);
        
        public bool DeleteComment(CommentModel comment);
        bool UpdateBlog (List<int> tagId, BlogModel blog);
        bool Save();
    }
}