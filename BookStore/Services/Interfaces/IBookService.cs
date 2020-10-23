using BookStore.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Api.Services
{
    public interface IBookService
    {
        Task<Book> Create(Book book);
        Task<Book> Delete(int id);
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Update(Book bookParam);
    }
}