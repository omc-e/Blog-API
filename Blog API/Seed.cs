using Blog_API.Data;
using Blog_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blog_API
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Blog_Tag.Any())
            {
                var blogTags = new List<Blog_TagModel>()
                {
                    new Blog_TagModel()
                    {
                        Blog = new BlogModel()
                        {
                            Slug = "augmented-reality-ios-application",
                            Title = "Augmented Reality iOS Application",
                            Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                            Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                            CreatedAt = DateTime.Now,
                            Comments = new List<CommentModel>()
                            {
                                new CommentModel
                                {   
                                    Body = "Great Blog.",
                                    CreatedAt= DateTime.Now,
                                },
                                new CommentModel
                                {
                                    Body = "Great Blog. My second comment.",
                                    CreatedAt= DateTime.Now,
                                }
                            }
                        },
                        Tag = new TagModel()
                        {
                        
                            TagName = "iOS"
                        },

                    },

                    new Blog_TagModel()
                    {
                        Blog = new BlogModel()
                        {
                            Slug = "augmented-reality-ios-application-demo",
                            Title = "Augmented Reality iOS Application Demo",
                            Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                            Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                            CreatedAt = DateTime.Now,
                            Comments = new List<CommentModel>()
                            {
                                new CommentModel
                                {
                                    Body = "Great Blog. My comment for second Post",
                                    CreatedAt= DateTime.Now,
                                },
                                new CommentModel
                                {
                                    Body = "Great Blog. My second comment for second post",
                                    CreatedAt= DateTime.Now,
                                }
                            }
                        },
                        Tag = new TagModel()
                        {
                          
                            TagName = "AR"
                        },

                    },

                };
                dataContext.Blog_Tag.AddRange(blogTags);
                dataContext.SaveChanges();
            }


        }

    }
}

