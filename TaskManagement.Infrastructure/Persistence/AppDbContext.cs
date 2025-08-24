using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Persistence
{
        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Team> Teams { get; set; }
            public DbSet<TaskEntity> Tasks { get; set; }

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configure User entity
                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(u => u.Id);
                    entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                    entity.Property(u => u.Email).IsRequired().HasMaxLength(255).IsUnicode(false);
                    entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
                    entity.Property(u => u.Role).HasConversion<string>().IsRequired();
                });

                // Configure Team entity
                modelBuilder.Entity<Team>(entity =>
                {
                    entity.HasKey(t => t.Id);
                    entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                    entity.Property(t => t.Description).HasMaxLength(500);
                    entity.HasMany(t => t.Members)
                        .WithOne()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Configure TaskEntity entity
                modelBuilder.Entity<TaskEntity>(entity =>
                {
                    entity.HasKey(t => t.Id);
                    entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
                    entity.Property(t => t.Description).HasMaxLength(1000);
                    entity.Property(t => t.Status).HasConversion<string>().IsRequired();
                    entity.HasOne(t => t.AssignedToUser)
                        .WithMany()
                        .HasForeignKey(t => t.AssignedToUserId)
                        .OnDelete(DeleteBehavior.Restrict);
                    entity.HasOne(t => t.CreatedByUser)
                        .WithMany()
                        .HasForeignKey(t => t.CreatedByUserId)
                        .OnDelete(DeleteBehavior.Restrict);
                    entity.HasOne(t => t.Team)
                        .WithMany()
                        .HasForeignKey(t => t.TeamId)
                        .OnDelete(DeleteBehavior.SetNull);
                });

                // Seed initial data
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        FullName = "Admin User",
                        Email = "admin@demo.com",
                        PasswordHash = "hashedAdmin123!", // Replace with actual hashed password using IPasswordService
                        Role = Role.Admin
                    },
                    new User
                    {
                        Id = 2,
                        FullName = "Manager User",
                        Email = "manager@demo.com",
                        PasswordHash = "hashedManager123!", // Replace with actual hashed password
                        Role = Role.Manager
                    },
                    new User
                    {
                        Id = 3,
                        FullName = "Employee User",
                        Email = "employee@demo.com",
                        PasswordHash = "hashedEmployee123!", // Replace with actual hashed password
                        Role = Role.Employee
                    }
                );
            }
        }
    
}
