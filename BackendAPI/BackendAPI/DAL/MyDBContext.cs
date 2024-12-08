using BackendAPI.Model;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace BackendAPI.DAL
{
    public class MyDBContext(DbContextOptions<MyDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userEntity = modelBuilder.Entity<User>();
            userEntity.ToCollection("users");
            userEntity.Property(p => p.Id).HasElementName("_id").HasConversion<ObjectId>();

        }
    }
}
