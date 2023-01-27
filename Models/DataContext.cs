﻿using Microsoft.EntityFrameworkCore;

namespace MSRCP_Server.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkData> WorkDatas { get; set; }
    }
}