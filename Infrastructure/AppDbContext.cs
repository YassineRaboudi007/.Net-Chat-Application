﻿using Microsoft.EntityFrameworkCore;
using ChatApplication.Domain.Entities;

namespace ChatApplication.Infrastructure
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
