using AutoMapper;
using Blog_API.DTO;
using Blog_API.Models;

namespace Blog_API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BlogModel, Blog>();
            CreateMap<CommentModel, Comment>();
            CreateMap<Comment, CommentModel>();
            CreateMap<TagModel, Tag>();
            CreateMap<Blog, BlogModel>();
            CreateMap<CreateBlog, BlogModel>();
            CreateMap<updateBlog, Blog>();
            
        }
    }
}
