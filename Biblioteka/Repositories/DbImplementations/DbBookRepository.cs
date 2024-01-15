using Biblioteka.Context;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Biblioteka.Repositories.DbImplementations
{
    public class DbBookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly BibContext _context;
       
        public DbBookRepository(BibContext context) : base(context)
        {
            _context = context;
        }

   
        public Book getOne(int id)
        {
            return _context.Book
                .Include(b => b.tags)
                .ThenInclude( b => b.tag)
                .Include(b => b.publisher)
                .Include(b => b.genre)
                .Include(b => b.type)
                .Include(b => b.authors)
                .ThenInclude(a => a.author).FirstOrDefault(b => b.bookId == id);
            
        }

        public Book getOne(string title)
        {
            return _context.Book
                .Include(b => b.tags)
                .ThenInclude(b => b.tag)
                .Include(b => b.publisher)
                .Include(b => b.genre)
                .Include(b => b.type)
                .Include(b => b.authors)
                .ThenInclude(a => a.author).FirstOrDefault(b => b.title == title);
        }

        public List<Book> getAll()
        {
            return _context.Book
              .Include(b => b.tags)
              .ThenInclude(b => b.tag)
              .Include(b => b.publisher)
              .Include(b => b.genre)
              .Include(b => b.type)
              .Include(b => b.authors)
             .ThenInclude(a => a.author).ToList();
        }

       /* public Book getBook(int id)
        {
            return _context.Book
                .Include(b => b.tags)
                .ThenInclude(b => b.tag)
                .Include(b => b.publisher)
                .Include(b => b.genre)
                .Include(b => b.type)
                .Include(b => b.authors)
                .ThenInclude(a => a.author).FirstOrDefault(b => b.bookId == id);

        }

        public Book getBook(string title)
        {
            return _context.Book
                .Include(b => b.tags)
                .ThenInclude(b => b.tag)
                .Include(b => b.publisher)
                .Include(b => b.genre)
                .Include(b => b.type)
                .Include(b => b.authors)
                .ThenInclude(a => a.author).FirstOrDefault(b => b.title == title);
        }*/

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            var query = _context.Book
                .Include(b => b.tags)
                .ThenInclude(b => b.tag)
                .Include(b => b.publisher)
                .Include(b => b.genre)
                .Include(b => b.type)
                .Include(b => b.authors)
                .ThenInclude(a => a.author)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchTerms = searchTerm.Split(',').Select(term => term.Trim().ToLower()).ToArray();

                // Pobierz dane z bazy danych do pamiêci
                var booksInMemory = query.ToList();

                var filteredBooks = booksInMemory
                    .Where(book =>
                        searchTerms.All(searchTerm =>
                            (book.title != null && book.title.ToLower().Contains(searchTerm)) ||
                            (book.ISBN.ToString() != null && book.ISBN.ToString().ToLower().Contains(searchTerm)) ||
                            (book.publisher != null && book.publisher.name.ToLower().Contains(searchTerm)) ||
                            (book.genre != null && book.genre.name.ToLower().Contains(searchTerm)) ||
                            (book.type != null && book.type.name.ToLower().Contains(searchTerm)) ||                          
                            book.authors.Any(a =>
                                (a.author.name != null && a.author.name.ToLower().Contains(searchTerm)) ||
                                (a.author.surname != null && a.author.surname.ToLower().Contains(searchTerm)) ||
                                (a.author.nickname != null && a.author.nickname.ToLower().Contains(searchTerm))) ||
                            book.tags.Any(t => t.tag.name != null && t.tag.name.ToLower().Contains(searchTerm)) ||
                            (DateTime.TryParseExact(searchTerm, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                             book.releaseDate.Date == date.Date))
                    )
                    .AsQueryable();

                // Zaktualizuj oryginalne zapytanie
                query = filteredBooks.AsQueryable();
            }

            return query.ToList();
        }
    }
}