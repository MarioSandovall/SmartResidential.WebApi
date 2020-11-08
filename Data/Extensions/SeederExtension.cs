using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Extensions
{
    internal static class SeederExtension
    {
        internal static void Seed<TEntity, TEnum>(this ModelBuilder modelBuilder)
            where TEntity : class, IEntity, new() where TEnum : Enum
        {
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                var instance = Activator.CreateInstance(typeof(TEntity), value);
                modelBuilder.Entity<TEntity>().HasData(instance);
            }
        }
    }
}
