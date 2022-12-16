using Blog_API.Models;

namespace Blog_API.Interfaces
{
    public interface ITagRepository
    {
        bool TagExist(int id);
        List<string> GetTags();


    }
}