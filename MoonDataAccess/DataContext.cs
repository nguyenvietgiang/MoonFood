﻿using Microsoft.EntityFrameworkCore;
using MoonModels;
using MoonModels.Paging;

namespace MoonDataAccess
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.UseSerialColumns();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Record> Records { get; set; }
    }
} 