using LibraryApi.Models;
using LibraryApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Autor> Autores { get; set; }
    public DbSet<Livro> Livros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            entity.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.Nacionalidade)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(a => a.DataCadastro)
                .IsRequired();

            entity.HasIndex(a => new { a.Nome, a.DataNascimento, a.Nacionalidade })
                .IsUnique();
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(l => l.Id);

            entity.Property(l => l.Id)
                .ValueGeneratedOnAdd();

            entity.Property(l => l.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(l => l.Preco)
                .HasColumnType("numeric(18,2)");

            entity.Property(l => l.Genero)
                .HasConversion<string>();

            entity.Property(l => l.DataCadastro)
                .IsRequired();

            entity.HasIndex(l => l.ISBN)
                .IsUnique();

            entity.HasOne(l => l.Autor)
                .WithMany(a => a.Livros)
                .HasForeignKey(l => l.AutorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
