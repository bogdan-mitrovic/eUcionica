using Microsoft.EntityFrameworkCore;
using DatabaseEntityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext
{
    public class DBContextClass : DbContext
    {
        public DbSet<Predmet> Predmet {  get; set; }    
        public DbSet<Pitanje> Pitanje { get; set; }
        public DbSet<Oblast> Oblast { get; set; }
        public DBContextClass(DbContextOptions<DBContextClass> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Predmet>()
                .Property(p => p.Ime)
                .IsRequired();


            modelBuilder.Entity<Pitanje>()
                .Property(z => z.Tezina)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
                .Property(z => z.Zadatak)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
                .Property(z => z.Odgovor)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
                .Property(z => z.OblastID)
                .IsRequired();
            modelBuilder.Entity<Pitanje>()
                .Property(z => z.PredmetID)
                .IsRequired();

            modelBuilder.Entity<Oblast>()
                .Property(o => o.Ime)
                .IsRequired();
            modelBuilder.Entity<Oblast>()
                .Property(o => o.PredmetID)
                .IsRequired();

            modelBuilder.Entity<Predmet>()
                .HasMany(s => s.PredmetnaPitanja).WithOne(p => p.Predmet).HasForeignKey(p => p.PredmetID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Predmet>()
                .HasMany(s => s.OblastPredmeta).WithOne(p =>p.Predmet).HasForeignKey(p => p.PredmetID)
                .OnDelete(DeleteBehavior.Cascade);  
            modelBuilder.Entity<Oblast>()
                .HasMany(s => s.PredmetnaOblast).WithOne(p => p.Oblast).HasForeignKey(p => p.OblastID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

    
}
