using Blog_API.Data;
using Blog_API.DTO;
using Blog_API.Interfaces;
using Blog_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Blog_API.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;

        public BlogRepository(DataContext context)
        {
            _context = context;
        }

        public bool BlogExists(string slug)
        {
            return _context.Blogs.Any(b => b.Slug == slug);
        }

        public Blog GetBlog(string slug)
        {
            var tempblog = _context.Blogs.Where(s => s.Slug == slug).Include(a => a.Blog_Tag).FirstOrDefault();
            var tags = _context.Blog_Tag.Where(e => e.Blog.Slug == tempblog.Slug).Select(b => b.Tag.TagName).ToList();
            tags = tags.Distinct().ToList();
            var blog = new Blog
            {
                Slug = tempblog.Slug,
                Title = tempblog.Title,
                Description = tempblog.Description,
                Body = tempblog.Body,
                CreatedAt = tempblog.CreatedAt,
                UpdatedAt = tempblog.UpdatedAt,
                Tags = tags.Distinct().ToList()
            };
            //return _context.Blogs.Where(b => b.Slug.Contains(slug) == true).FirstOrDefault();
            return blog;
        }

        public List<Blog> GetBlogs()
        {
            var blogList = _context.Blogs.OrderBy(b => b.Slug).ToList();
            var data = new List<Blog>();
            for (int i = 0; i < blogList.Count; i++)
            {
                var tags = _context.Blog_Tag.Where(e => e.Blog.Slug == blogList[i].Slug).Select(b => b.Tag.TagName).ToList();
                tags = tags.Distinct().ToList();
                var blog = new Blog
                {
                    Slug = blogList[i].Slug,
                    Title = blogList[i].Title,
                    Description = blogList[i].Description,
                    Body = blogList[i].Body,
                    CreatedAt = blogList[i].CreatedAt,
                    UpdatedAt = blogList[i].UpdatedAt,
                    Tags = tags
                };
                data.Add(blog);
            }

            return data;
        }
        
        public List<Blog> GetBlogFilteredByTag(string tagName)
        {
            var blogList = _context.Blog_Tag.Where(e => e.Tag.TagName == tagName).Select(b => b.Blog).ToList();
            var data = new List<Blog>();
            for (int i = 0; i < blogList.Count; i++)
            {
                var tags = _context.Blog_Tag.Where(e => e.Blog.Slug == blogList[i].Slug).Select(b => b.Tag.TagName).ToList();
                tags = tags.Distinct().ToList();
                var blog = new Blog
                {
                    Slug = blogList[i].Slug,
                    Title = blogList[i].Title,
                    Description = blogList[i].Description,
                    Body = blogList[i].Body,
                    CreatedAt = blogList[i].CreatedAt,
                    UpdatedAt = blogList[i].UpdatedAt,
                    Tags = tags
                };
                data.Add(blog);
            }

            return data;
        }

        public List<CommentModel> GetComment(string slug)
        {
            var blog = _context.Blogs.Where(b => b.Slug == slug).FirstOrDefault();
            var comment = _context.Comments.Where(c => c.BlogId == blog.Id).ToList();
            

            if (comment.Count() <= 0)
            {
                return null;
            }

            return comment.Select(k => new CommentModel { CommentId = k.CommentId, Body = k.Body, CreatedAt = k.CreatedAt, UpdatedAt = k.UpdatedAt }).ToList();


        }

        public bool TagExist(string tagName)
        {
            return _context.Tags.Any(t => t.TagName == tagName);
        }

      

        public bool Save()
        {
           
            var saved = _context.SaveChanges();
           

            return saved > 0 ? true : false;
        }

        public bool CreateComment(CommentModel comment)
        {
            _context.Comments.Add(comment);
            return Save();
        }


        public string GenerateSlug(string title)
        {
            if (string.IsNullOrEmpty(title)) return title;

            title = title.Normalize(NormalizationForm.FormD);
            char[] chars = title.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

            var finalTitle = new string(chars).Normalize(NormalizationForm.FormC);
            finalTitle = finalTitle.ToLower();
            finalTitle = Regex.Replace(finalTitle, @"[^A-Za-z0-9\s-]", "");
            finalTitle = Regex.Replace(finalTitle, @"\s+", " ").Trim();
            finalTitle = Regex.Replace(finalTitle, @"\s", "-");

            return finalTitle;
        }

        public bool UpdateBlog(List<int> tagId, BlogModel blog)
        {
            _context.Update(blog);
            return Save();
        }

        public bool DeleteComment(CommentModel comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public bool DeleteBlog(BlogModel blog)
        {
            _context.Entry(blog).State = EntityState.Deleted;
            _context.Remove(blog);

            return Save();
        }
    }
}