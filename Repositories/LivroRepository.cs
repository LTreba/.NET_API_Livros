using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly LibraryDbContext _context;

    public LivroRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Livro?> GetByIdAsync(Guid id)
    {
        return await _context.Livros
            .Include(l => l.Autor)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IList<Livro>> SearchAsync(string? isbn, string? titulo, string? nomeAutor, GeneroLivro? genero, int? anoPublicacao)
    {
        var query = _context.Livros.Include(l => l.Autor).AsQueryable();

        if (!string.IsNullOrEmpty(isbn))
            query = query.Where(l => l.ISBN.Contains(isbn));

        if (!string.IsNullOrEmpty(titulo))
            query = query.Where(l => l.Titulo.Contains(titulo));

        if (!string.IsNullOrEmpty(nomeAutor))
            query = query.Where(l => l.Autor.Nome.Contains(nomeAutor));

        if (genero.HasValue)
            query = query.Where(l => l.Genero == genero);

        if (anoPublicacao.HasValue)
            query = query.Where(l => l.DataPublicacao.Year == anoPublicacao);

        return await query.ToListAsync();
    }

    public async Task AddAsync(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Livro livro)
    {
        _context.Livros.Update(livro);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Livro livro)
    {
        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsIsbnAsync(string isbn, Guid? ignoreId = null)
    {
        return await _context.Livros.AnyAsync(l =>
            l.ISBN == isbn &&
            l.Id != ignoreId);
    }
}
