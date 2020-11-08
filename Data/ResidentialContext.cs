using Data.Configurations;
using Data.Extensions;
using Domain.Entities;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ResidentialContext : DbContext
    {
        public DbSet<UserEf> Users { get; set; }

        public DbSet<RoleEf> Roles { get; set; }
        
        public DbSet<AreaEf> Areas { get; set; }
        
        public DbSet<HouseEf> Houses { get; set; }
        
        public DbSet<ReportEf> Reports { get; set; }
        
        public DbSet<ActionEf> Actions { get; set; }
        
        public DbSet<PaymentEf> Payments { get; set; }
        
        public DbSet<ExpenseEf> Expenses { get; set; }
        
        public DbSet<UserRoleEf> UserRoles { get; set; }
        
        public DbSet<HouseUserEf> HouseUsers { get; set; }
        
        public DbSet<AttachmentEf> Attachments { get; set; }
        
        public DbSet<ResidentialEf> Residentials { get; set; }
        
        public DbSet<PaymentTypeEf> PaymentTypes { get; set; }
        
        public DbSet<ReservationEf> Reservations { get; set; }
        
        public DbSet<ReportStatusEf> ReportStatus { get; set; }
        
        public DbSet<AnnouncementEf> Announcements { get; set; }
        
        public DbSet<PaymentStatusEf> PaymentStatus { get; set; }
        
        public DbSet<AdvertisementEf> Advertisements { get; set; }
        
        public DbSet<ReportMessageEf> ReportMessages { get; set; }
        
        public DbSet<AttachmentTypeEf> AttachmentTypes { get; set; }
        
        public DbSet<PaymentCategoryEf> PaymentCategories { get; set; }
        
        public DbSet<ExpenseCategoryEf> ExpenseCategories { get; set; }
        
        public DbSet<ResidentialStatusEf> ResidentialStatus { get; set; }
        
        public DbSet<ReservationStatusEf> ReservationStatus { get; set; }

        public ResidentialContext(DbContextOptions<ResidentialContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurations
            UserRoleConfiguration.Configure(modelBuilder);
            HouseUserConfiguration.Configure(modelBuilder);

            //Seeders
            modelBuilder.Seed<RoleEf, RoleEnum>();
            modelBuilder.Seed<PaymentTypeEf, PaymentTypeEnum>();
            modelBuilder.Seed<ReportStatusEf, ReportStatusEnum>();
            modelBuilder.Seed<PaymentStatusEf, PaymentStatusEnum>();
            modelBuilder.Seed<AttachmentTypeEf, AttachmentTypeEnum>();
            modelBuilder.Seed<ReservationStatusEf, ReservationStatusEnum>();
            modelBuilder.Seed<ResidentialStatusEf, ResidentialStatusEnum>();

            base.OnModelCreating(modelBuilder);
        }

    }


}
