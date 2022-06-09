using Microsoft.EntityFrameworkCore;
using cs_app.Data.Models;

namespace cs_app.Data
{
    public class EducationContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //это метод, вызываемый при инициализации экземпляра класса EducationContext,
        //который отвечает за первичное подключение к БД и ее настройку.
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Host=localhost;
                                                    Database=education;
                                                    Username=postgres;
                                                    Password=root;
                                                    Port=5432")
                .UseSnakeCaseNamingConvention()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.
                    AddConsole())).EnableSensitiveDataLogging();
        }

        //метод определения параметров при работе с сущностями и таблицами в БД
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ticket>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Hotel>().HasMany(au => au.Tickets).
                WithMany(af => af.Hotels);
            modelBuilder.Entity<Customer>().HasMany(ar => ar.Hotels);
        }
    }
}