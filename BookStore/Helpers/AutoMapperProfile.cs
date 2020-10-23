using AutoMapper;
using BookStore.Api.Entities;
using BookStore.Api.Models.Books;
using BookStore.Api.Models.Users;

namespace BookStore.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<Models.Users.UpdateModel, User>();
            
            CreateMap<Book, BookModel>();
            CreateMap<CreateModel, Book>();
            CreateMap<Models.Books.UpdateModel, Book>();
        }
    }
}