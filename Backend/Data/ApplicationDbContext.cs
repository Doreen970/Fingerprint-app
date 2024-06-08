using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<Staff>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Transaction and Client: Many-to-One relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Client)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.ClientId);

            // Transaction and Staff: Many-to-One relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Staff)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.StaffId);

            // Staff and Privilege: One-to-One relationship
            //modelBuilder.Entity<Staff>()
            //    .HasOne(s => s.Privilege)
            //    .WithOne()
            //    .HasForeignKey<Staff>(s => s.PrivilegeId);

            // Device and Staff: Many-to-One relationship (Nullable StaffID)
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Staff)
                .WithMany(s => s.Devices)
                .HasForeignKey(d => d.StaffId)
                .IsRequired(false);

            // Account and Client: One-to-One relationship
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Account)
                .WithOne(a => a.Client)
                .HasForeignKey<Account>(a => a.ClientId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
