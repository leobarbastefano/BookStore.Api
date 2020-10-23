using BookStore.Api.Entities;
using BookStore.Api.Helpers;
using BookStore.Api.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Xunit.Test
{
    public class BookTest
    {
        [Fact]
        public void TestCreateBook()
        {
            Book _book = new Book();

            _book.Title = "ABC";
            _book.Author = "Robert";
            _book.Year = 2010;
            _book.Price = 15.99M;

            var book = BookService.CreateCheck(_book);

            Assert.Equal("ABC", book.Title);
            Assert.Equal("Robert", book.Author);
            Assert.Equal(2010, book.Year);
            Assert.Equal(15.99M, book.Price);

        }

        [Fact]
        public void TestUpdateBook()
        {
            Book _book = new Book();

            _book.Title = "ABC";
            _book.Author = "Robert";
            _book.Year = 2010;
            _book.Price = 15.99M;

            var book = _book;
            var bookReturn = BookService.UpdateCheck(_book, book);

            Assert.Equal("ABC", book.Title);
            Assert.Equal("Robert", book.Author);
            Assert.Equal(2010, book.Year);
            Assert.Equal(15.99M, book.Price);

        }


    }
}
