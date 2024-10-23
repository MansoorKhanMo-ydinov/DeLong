﻿using DeLong.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DeLong.DbContexts;

public class AppdbContext:DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // PostgreSQL ulanish satrini kiriting
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=delongdb;Username=postgres;Password=mansurjon1607");
    }
}
