using Blog_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Blog_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }

            }
            catch
            {
                
            }
        }

        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<Blog_TagModel> Blog_Tag { get; set; }

        public DbSet<TagModel> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BlogModel>().Navigation(f => f.Blog_Tag).AutoInclude();

            modelBuilder.Entity<Blog_TagModel>()
                .HasOne(b => b.Blog)
                .WithMany(bt => bt.Blog_Tag)
                .HasForeignKey(s => s.BlogId);

            modelBuilder.Entity<Blog_TagModel>()
               .HasOne(b => b.Tag)
               .WithMany(bt => bt.Blog_Tag)
               .HasForeignKey(s => s.TagId);
        }

    }
}
