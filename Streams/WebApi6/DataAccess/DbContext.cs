﻿using Microsoft.EntityFrameworkCore;

namespace WebAPI6
{
    public class ArticleDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.ConnectionString);
        }
    }
}