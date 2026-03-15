using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly LibraryDbContext _context;

    public AutorRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Autor?> GetByIdAsync(Guid id)
    {
        return await _context.Autores.FindAsync(id);
    }

    public async Task<IList<Autor>> SearchAsync(string? nome, string? nacionalidade)
    {
        var query = _context.Autores.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(a => a.Nome.Contains(nome));

        if (!string.IsNullOrEmpty(nacionalidade))
            query = query.Where(a => a.Nacionalidade.Contains(nacionalidade));

        return await query.ToListAsync();
    }

    public async Task AddAsync(Autor autor)
    {
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Autor autor)
    {
        _context.Autores.Update(autor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Autor autor)
    {
        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string nome, DateOnly dataNascimento, string nacionalidade, Guid? ignoreId = null)
    {
        return await _context.Autores.AnyAsync(a =>
            a.Nome == nome &&
            a.DataNascimento == dataNascimento &&
            a.Nacionalidade == nacionalidade &&
            a.Id != ignoreId);
    }

    public async Task<bool> HasLivrosAsync(Guid id)
    {
        return await _context.Livros.AnyAsync(l => l.AutorId == id);
    }
}
