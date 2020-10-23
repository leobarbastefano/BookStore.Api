using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Api.Entities;
using BookStore.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Services
{
    public class BookService : IBookService
    {
        private DataContext _context;

        public BookService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await Task.FromResult(_context.Books);
        }

        public async Task<Book> GetById(int id)
        {
            return await Task.FromResult(_context.Books.Find(id));
        }

        public async Task<Book> Create(Book book)
        {
            book = CreateCheck(book);

            if (_context.Books.Any(x => x.Title == book.Title))
                throw new AppException($"Book Title { book.Title } is already in database");

            _context.Books.Add(book);
            _context.SaveChanges();

            return await Task.FromResult(book);
        }

        public async Task<Book> Update(Book bookParam)
        {
            var book = await _context.Books.FindAsync(bookParam.Id);

            if (!string.IsNullOrWhiteSpace(bookParam.Title) && bookParam.Title != book.Title)
            {
                if (await _context.Books.AnyAsync(x => x.Title == bookParam.Title))
                    throw new AppException($"Book  { bookParam.Title } is already in database");

                book.Title = bookParam.Title;
            }

            UpdateCheck(bookParam, book);

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return await Task.FromResult(book);
        }

        public async Task<Book> Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return await Task.FromResult(book);
        }

        // private methods

        public static Book CreateCheck(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new AppException("Title is required");

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new AppException("Author is required");

            if (book.Year == 0)
                throw new AppException("Book year is required");

            if (book.Price == 0)
                throw new AppException("Book price cannot be zero");

            return book;
        }
        public static Book UpdateCheck(Book book, Book bookParam)
        {
            if (book == null)
                throw new AppException("Book not found");

            if (!string.IsNullOrWhiteSpace(bookParam.Title))
                book.Title = bookParam.Title;

            if (!string.IsNullOrWhiteSpace(bookParam.Author))
                book.Author = bookParam.Author;

            if (IsInt(bookParam.Year.ToString()))
                book.Year = bookParam.Year;

            if (IsDecimal(bookParam.Price.ToString()))
                book.Price = bookParam.Price;

            return book;
        }

        public static bool IsInt(string text)
        {
            int test;
            return int.TryParse(text, out test);
        }

        public static bool IsDecimal(string text)
        {
            decimal test;
            return decimal.TryParse(text, out test);
        }

    }
}