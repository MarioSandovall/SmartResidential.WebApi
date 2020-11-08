using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Configurations
{
    internal static class HouseUserConfiguration
    {
        internal static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseUserEf>()
                .HasKey(hu => new { hu.HouseId, hu.UserId });

            modelBuilder.Entity<HouseUserEf>()
                .HasOne(h => h.House)
                .WithMany(hu => hu.HouseUsers)
                .HasForeignKey(h => h.HouseId);

            modelBuilder.Entity<HouseUserEf>()
                .HasOne(h => h.User)
                .WithMany(hu => hu.HouseUsers)
                .HasForeignKey(h => h.UserId);
        }
    }
}
