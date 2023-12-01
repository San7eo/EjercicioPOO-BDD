using EjercicioPOO_BDD.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPOO_BDD.Repository
{
    public class DataBaseContext : DbContext
    {
        //string StringConnection = @"Data Source=localhost;Initial Catalog=EjecicioPOO+BDD;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(StringConnection);

        //    base.OnConfiguring(optionsBuilder);
        //}
        public DbSet<CParametria> Parametria { get; set; }
        public DbSet<CAceptado> Aceptado { get; set; }
        
        public DbSet<CRechazado> Rechazado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CParametria>(entity =>
            {
                entity.ToTable("parametria");

                entity.Property(para => para.IdFecha)
                      .HasColumnName("id");


                entity.Property(para => para.Fecha_Proceso)
                      .HasColumnName("fecha_proceso");

                entity.HasKey(para => para.IdFecha);

            });

            modelBuilder.Entity<CAceptado>(entity =>
            {
                entity.ToTable("ventas_mensuales");

                entity.Property(aceptado => aceptado.IdAceptado)
                      .HasColumnName("id");

                entity.Property(acep => acep.FechaInforme)
                      .HasColumnName("fecha_informe");

                entity.Property(acep => acep.CodigoVendedor)
                      .HasColumnName("codigo_vendedor");

                entity.Property(acep => acep.Venta)
                      .HasColumnName("venta");

                entity.Property(acep => acep.TamañoEmpresa)
                      .HasColumnName("tamañoEmpresa");

                entity.HasKey(aceptado => aceptado.IdAceptado);

            });

            modelBuilder.Entity<CRechazado>(entity =>
            {
                entity.ToTable("rechazos");

                entity.Property(recha => recha.IdRechazado)
                       .HasColumnName("id");

                entity.Property(recha => recha.Motivo)
                      .HasColumnName("informe");

                entity.HasKey(recha => recha.IdRechazado);

            });

            base.OnModelCreating(modelBuilder);
        }
    }

    
}
