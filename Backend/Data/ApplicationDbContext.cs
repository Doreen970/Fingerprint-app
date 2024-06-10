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
        public DbSet<Services> Services { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

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

            // Transaction and Service: Many-to-One relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Service)
                .WithMany()
                .HasForeignKey(t => t.ServiceID);

            //// Device and Staff: Many-to-One relationship (Nullable StaffID)
            //modelBuilder.Entity<Device>()
            //    .HasOne(d => d.Staff)
            //    .WithMany(s => s.Devices)
            //    .HasForeignKey(d => d.StaffId)
            //    .IsRequired(false);

            // Device and Staff: One-to-One relationship
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Staff)
                .WithOne(s => s.Device)
                .HasForeignKey<Device>(d => d.StaffId)
                .IsRequired(false);

            // Account and Client: One-to-One relationship
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Account)
                .WithOne(a => a.Client)
                .HasForeignKey<Account>(a => a.ClientId);

            modelBuilder.Entity<Loan>()
               .HasOne(l => l.Client)
               .WithMany(c => c.Loans)
               .HasForeignKey(l => l.ClientId)
               .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Account)
                .WithMany() 
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial services
            modelBuilder.Entity<Services>().HasData(
                new Services { ServiceID = 1, ServiceName = "Customer Registration", ServiceCode = "CUST_REG", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 2, ServiceName = "Cash Deposit", ServiceCode = "CASH_DEP", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 3, ServiceName = "Cash Withdrawal", ServiceCode = "CASH_WDL", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 4, ServiceName = "ATM Registration", ServiceCode = "ATM_REG", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 5, ServiceName = "Edit Customer Details", ServiceCode = "EDIT_CUST", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 6, ServiceName = "Invoice Printing", ServiceCode = "INV_PRINT", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 7, ServiceName = "Loan Disbursement", ServiceCode = "LOAN_DISB", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 8, ServiceName = "Cheque Receive", ServiceCode = "CHQ_RCV", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 9, ServiceName = "Currency Exchange", ServiceCode = "CURR_EXCH", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow },
                new Services { ServiceID = 10, ServiceName = "Delete Customer", ServiceCode = "DEL_CUST", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow }
            );

            // Seed initial roles
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
