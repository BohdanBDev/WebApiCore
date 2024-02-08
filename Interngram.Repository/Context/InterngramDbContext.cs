using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interngram.Repository.Models;

namespace Interngram.Repository.Context
{
    public class InterngramDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "https://interngram.documents.azure.com:443/",
                "LgzQVXgcwBM1DomwsJ5ekJfek2gheenwl28071c94UjHnx9DSQBDbjk1lk0CXtTcZfECGOrJoBhSH5czVaEXxQ==",
                "interngram-db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToContainer("Users")
                .HasPartitionKey(u => u.Id);
            modelBuilder.Entity<Post>()
                .ToContainer("Posts")
                .HasPartitionKey(u => u.Id);
        }
    }
}
