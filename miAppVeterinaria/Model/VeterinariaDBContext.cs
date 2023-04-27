using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace miAppVeterinaria.Model
{
    public class VeterinariaDBContext : DbContext
    {
        //Esta clase sirve para hacer los mapeos
        public VeterinariaDBContext(DbContextOptions<VeterinariaDBContext> options) : base(options)
        {
        }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Veterinario> veterinarios { get; set; }
        public DbSet<Secretaria> secretarias { get; set; }
        public DbSet<Consultorio> consultorios { get; set; }
        public DbSet<SecretariaVeterinarios> secretariaVeterinarios { get; set; }
        public DbSet<DiasLaboralesVeterinarios> diasLaboralesVet { get; set; }
        public DbSet<DiasLaboralesSecretarias> diasLaboralesSec { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Veterinario>()
            //    .HasOne(a => a.cliente)
            //    .WithOne(a => a.veterinario)
            //    .HasForeignKey<Cliente>(c => c.VeterinarioId);

            //modelBuilder.Entity<Country>()
            //.HasOne(a => a.CapitalCity)
            //.WithOne(a => a.Country)
            //.HasForeignKey<CapitalCity>(c => c.CountryID);
        }
    }
}
