using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class AuctionDbContext : DbContext
    {
        public AuctionDbContext()
        {
        }
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
     => optionsBuilder.UseSqlServer("Server=DESKTOP-3OUFFGR\\MSSQLSERVER01;Database=Auctions;Trusted_Connection=True;TrustServerCertificate=True;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure User-Auction relationship
            modelBuilder.Entity<Auction>()
                .HasOne(a => a.User)
                .WithMany(u => u.Auctions)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure User-Bid relationship
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure Auction-Bid relationship
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<UserRole>()
               .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<Auction>()
                .Property(a => a.StartPrice)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<Bid>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<User>()
                .Property(u => u.Wallet)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed
        }
    }
}

