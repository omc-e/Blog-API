using Blog_API.Data;
using Blog_API.Interfaces;
using Blog_API.Models;
using System.Diagnostics;

namespace Blog_API.Repository
{
    public class TagRepository : ITagRepository
    {
        private DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }



        public List<string> GetTags()
        {
            List<string> tags = _context.Tags.Select(t => t.TagName).ToList();

            tags = tags.Distinct().ToList();

            return tags;
        }

        public bool TagExist(int id)
        {
            return _context.Tags.Any();
        }
    }
}