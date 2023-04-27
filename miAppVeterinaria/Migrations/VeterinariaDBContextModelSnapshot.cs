﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using miAppVeterinaria.Model;

#nullable disable

namespace miAppVeterinaria.Migrations
{
    [DbContext(typeof(VeterinariaDBContext))]
    partial class VeterinariaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("miAppVeterinaria.Model.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("consultorioIdConsultorio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("consultorioIdConsultorio");

                    b.ToTable("clientes");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.Consultorio", b =>
                {
                    b.Property<int>("IdConsultorio")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdConsultorio");

                    b.ToTable("consultorios");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.DiasLaboralesSecretarias", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("Domingo")
                        .HasColumnType("bit");

                    b.Property<bool>("Jueves")
                        .HasColumnType("bit");

                    b.Property<bool>("Lunes")
                        .HasColumnType("bit");

                    b.Property<bool>("Martes")
                        .HasColumnType("bit");

                    b.Property<bool>("Miercoles")
                        .HasColumnType("bit");

                    b.Property<bool>("Sabado")
                        .HasColumnType("bit");

                    b.Property<bool>("Viernes")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("diasLaboralesSec");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.DiasLaboralesVeterinarios", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("Domingo")
                        .HasColumnType("bit");

                    b.Property<bool>("Jueves")
                        .HasColumnType("bit");

                    b.Property<bool>("Lunes")
                        .HasColumnType("bit");

                    b.Property<bool>("Martes")
                        .HasColumnType("bit");

                    b.Property<bool>("Miercoles")
                        .HasColumnType("bit");

                    b.Property<bool>("Sabado")
                        .HasColumnType("bit");

                    b.Property<bool>("Viernes")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("diasLaboralesVet");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.Secretaria", b =>
                {
                    b.Property<int>("IdSecretaria")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Consultorio")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSecretaria");

                    b.ToTable("secretariasVeterinarios");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.SecretariaVeterinarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SecretariaId")
                        .HasColumnType("int");

                    b.Property<int>("VeterinarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("secretariaVeterinarios");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.Veterinario", b =>
                {
                    b.Property<int>("IdVeterinario")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("consultorio")
                        .HasColumnType("int");

                    b.HasKey("IdVeterinario");

                    b.ToTable("veterinarios");
                });

            modelBuilder.Entity("miAppVeterinaria.Model.Cliente", b =>
                {
                    b.HasOne("miAppVeterinaria.Model.Consultorio", "consultorio")
                        .WithMany()
                        .HasForeignKey("consultorioIdConsultorio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("consultorio");
                });
#pragma warning restore 612, 618
        }
    }
}
