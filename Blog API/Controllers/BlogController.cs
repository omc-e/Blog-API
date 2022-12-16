using AutoMapper;
using Blog_API.Data;
using Blog_API.DTO;
using Blog_API.Interfaces;
using Blog_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Web.Http.ModelBinding;

namespace Blog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private DataContext _context;

        public BlogController(IBlogRepository blogRepository, IMapper mapper, DataContext context)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _context = context;
        }

        //GET ALL BLOGS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Blog>))]
        public IActionResult GetBlogs(string? tagName)
        {
            if (tagName != null)
            {
                var blog = _mapper.Map<List<Blog>>(_blogRepository.GetBlogFilteredByTag(tagName));

                if (blog.Count <= 0)
                {
                    return NotFound($"Blog with tag {tagName} not found.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data1 = new BlogPosts
                {
                    Blogs = blog,
                    recodCount = blog.Count
                };

                return Ok(data1);
            }
           
                var blogs = _mapper.Map<List<Blog>>(_blogRepository.GetBlogs());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = new BlogPosts
                {
                    Blogs = blogs,
                    recodCount = blogs.Count
                };
                return Ok(data);
            
           
        }

        
        //public IActionResult GetBlogsByTag([FromQuery] string tagName)
        //{
        //    //if (_blogRepository.TagExist(tagName))
        //    //{
        //    //    return NotFound("There is no tag in the system.");
        //    //}
            
        //}


        //GET ONE BLOG USING SLUG
        [HttpGet("{slug}")]
        [ProducesResponseType(200, Type = typeof(Blog))]
        [ProducesResponseType(400)]
        public IActionResult GetBlog(string slug)
        {
            if (!_blogRepository.BlogExists(slug))
                return NotFound();

            var blog = _blogRepository.GetBlog(slug);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(blog);
        }

        //GET COMMMENTS
        [HttpGet("{slug}/comments")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComments(string slug)
        {
            if (!_blogRepository.BlogExists(slug))
            {
                return NotFound();
            }
            var comments = _mapper.Map<List<Comment>>(_blogRepository.GetComment(slug));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(comments);
        }

        [HttpPost("{slug}/comments")]
        public IActionResult CreateComment(string slug, [FromBody]Comment comment)
        {
            var blog = _context.Blogs.Where(b=> b.Slug == slug).FirstOrDefault();
            if (blog == null) return NotFound();

            var a = new CommentModel
            {
                BlogId = blog.Id,
                Body = comment.Body,
                CreatedAt = DateTime.Now
                //Blog = blog

            };

            if (!_blogRepository.CreateComment(a))
            {
                
                return StatusCode(500, ModelState);
            }
            
            return Ok(comment);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> CreateBlog([FromBody]CreateBlog createBlog)
        {

            if (createBlog.Title == null || createBlog.Description == null || createBlog.Body == null) return BadRequest();
            var finalBlog = _mapper.Map<BlogModel>(createBlog);
            finalBlog.CreatedAt = DateTime.Now;
            finalBlog.Slug = _blogRepository.GenerateSlug(finalBlog.Title);
            var blogTagModel = new List<Blog_TagModel>();
            var tagList = createBlog.Tags;
            var tags = new List<TagModel>();
            for(int i=0; i<tagList.Length; i++)
            {
                var temp = _context.Tags.Where(t => t.TagName == tagList[i]).FirstOrDefault();
                if(temp == null)
                {
                    var a = new TagModel { TagName = tagList[i] };
                     _context.Tags.Add(a);
                    _context.SaveChanges();
                    tags.Add(a);
                }
                else
                {
                    tags.Add(temp);
                }
                
            }
            

            for (int i=0; i< createBlog.Tags.Length; i++)
            {

                    var blogTag = new Blog_TagModel
                    {
                        TagId = tags[i].TagId,
                        Blog = finalBlog
                    };
                    blogTagModel.Add(blogTag);

            }

            _context.Blog_Tag.AddRange(blogTagModel);
          

            _context.Add(finalBlog);
            _context.SaveChanges();
            return Ok("CREATED");
        }

        [HttpPut("{slug}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBlog(string slug, [FromBody] updateBlog updateBlog)
        {
            if (updateBlog == null) return BadRequest(ModelState);
            if (!_blogRepository.BlogExists(slug)) return NotFound("Blog does not exist!");

            var blog = _context.Blogs.Where(t => t.Slug == slug).First();
           var id = _context.Blog_Tag.Where(e => e.Blog.Slug == blog.Slug).Select(b => b.Tag.TagId).ToList();
            if (blog != null)
            {
                if (!String.IsNullOrEmpty(updateBlog.Title))
                {
                    blog.Title = updateBlog.Title;
                    blog.Slug = _blogRepository.GenerateSlug(updateBlog.Title);
                }
                if (!String.IsNullOrEmpty(updateBlog.Description))
                {
                    blog.Description = updateBlog.Description;
                }
                if (!String.IsNullOrEmpty(updateBlog.Body))
                {
                    blog.Body = updateBlog.Body;
                }

                blog.UpdatedAt = DateTime.Now;

            }

            if (!_blogRepository.UpdateBlog(id, blog))
            {
                return StatusCode(500, ModelState);
            }
            

            return Ok();
        }

        [HttpDelete("{slug}/comments/{id}")]
        public IActionResult DeleteComment(int id)
        {
            if(!_context.Comments.Any(c => c.CommentId == id)) return NotFound();

            var commentToDelete = _context.Comments.Where(c => c.CommentId == id).FirstOrDefault();
            if (!_blogRepository.DeleteComment(commentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }
            return NoContent();
        }

        [HttpDelete("{slug}")]
        public async Task<ActionResult> DeleteBlog(string slug)
        {
            if (!_blogRepository.BlogExists(slug)) return NotFound();
            var blog = _context.Blogs.Where(b => b.Slug == slug).FirstOrDefault();

               _context.Blog_Tag.RemoveRange(blog.Blog_Tag);
            if(_blogRepository.GetComment(slug) != null)
               _context.Comments.RemoveRange(_blogRepository.GetComment(slug));

            //    _context.Entry(blogToDelete).State = EntityState.Deleted;
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
