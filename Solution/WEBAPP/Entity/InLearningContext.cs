using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WEBAPP.Entity
{
    public partial class InLearningContext : DbContext
    {
        public InLearningContext()
        {
        }

        public InLearningContext(DbContextOptions<InLearningContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<Curso> Cursos { get; set; } = null!;
        public virtual DbSet<Notum> Nota { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=HOST;Initial Catalog=InLearning;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno);

                entity.ToTable("Alumno");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Dni)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("DNI");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.IdCurso);

                entity.ToTable("Curso");

                entity.Property(e => e.Docente)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notum>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK_DetalleSolicitud");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotasAlumno");

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdCurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotasCurso");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
