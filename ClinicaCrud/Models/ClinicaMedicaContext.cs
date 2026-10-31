using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClinicaCrud.Models
{
    public partial class ClinicaMedicaContext : DbContext
    {
        public ClinicaMedicaContext()
        {
        }

        public ClinicaMedicaContext(DbContextOptions<ClinicaMedicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consultum> Consulta { get; set; } = null!;
        public virtual DbSet<Exame> Exames { get; set; } = null!;
        public virtual DbSet<Medico> Medicos { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consultum>(entity =>
            {
                entity.HasKey(e => e.ConsultaId)
                    .HasName("PK__Consulta__7D0B7DACB09B6EB8");

                entity.Property(e => e.ConsultaId).HasColumnName("ConsultaID");

                entity.Property(e => e.DataHora).HasColumnType("datetime");

                entity.Property(e => e.MedicoId).HasColumnName("MedicoID");

                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.HasOne(d => d.Medico)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.MedicoId)
                    .HasConstraintName("FK__Consulta__Medico__2A4B4B5E");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.PacienteId)
                    .HasConstraintName("FK__Consulta__Pacien__2B3F6F97");
            });

            modelBuilder.Entity<Exame>(entity =>
            {
                entity.ToTable("Exame");

                entity.Property(e => e.ExameId).HasColumnName("ExameID");

                entity.Property(e => e.ConsultaId).HasColumnName("ConsultaID");

                entity.Property(e => e.DataExame).HasColumnType("date");

                entity.Property(e => e.TipoExame).HasMaxLength(100);

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Exames)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK__Exame__ConsultaI__2E1BDC42");
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medico");

                entity.HasIndex(e => e.Crm, "UQ__Medico__C1F887FFC7161616")
                    .IsUnique();

                entity.Property(e => e.MedicoId).HasColumnName("MedicoID");

                entity.Property(e => e.Crm)
                    .HasMaxLength(20)
                    .HasColumnName("CRM");

                entity.Property(e => e.Especialidade).HasMaxLength(100);

                entity.Property(e => e.Nome).HasMaxLength(100);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Paciente");

                entity.HasIndex(e => e.Cpf, "UQ__Paciente__C1F89731ACB2C2D4")
                    .IsUnique();

                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(14)
                    .HasColumnName("CPF");

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Nome).HasMaxLength(100);

                entity.Property(e => e.Telefone).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
