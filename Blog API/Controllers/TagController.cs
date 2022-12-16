using AutoMapper;
using Blog_API.DTO;
using Blog_API.Interfaces;
using Blog_API.Models;
using Blog_API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace Blog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private ITagRepository _tagRepository;
        private IMapper _mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTags()
        {
            var tags = _tagRepository.GetTags();

            if(tags.Count <= 0)
            {
                return NotFound("There is no tags in the system");
            }

            return Ok(tags);
        }
    }
}
