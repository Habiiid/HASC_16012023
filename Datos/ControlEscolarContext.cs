using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datos;

public partial class ControlEscolarContext : DbContext
{
    public ControlEscolarContext()
    {
    }

    public ControlEscolarContext(DbContextOptions<ControlEscolarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnoMaterium> AlumnoMateria { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= ControlEscolar; Trusted_Connection=True; User ID=sa; Password=pass@word1;TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumno__460B474094298A09");

            entity.ToTable("Alumno");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AlumnoMaterium>(entity =>
        {
            entity.HasKey(e => e.IdAlumnoMateria).HasName("PK__AlumnoMa__E13EDA373FC7ECFA");

            entity.HasOne(d => d.IdAlumnoNavigation).WithMany(p => p.AlumnoMateria)
                .HasForeignKey(d => d.IdAlumno)
                .HasConstraintName("AlumnoMateriaAlumno");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.AlumnoMateria)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("AlumnoMateriaMateria");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PK__Materia__EC174670DF59FA11");

            entity.Property(e => e.Costo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
