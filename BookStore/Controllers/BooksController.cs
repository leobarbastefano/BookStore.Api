using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using BookStore.Api.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BookStore.Api.Services;
using BookStore.Api.Entities;
using BookStore.Api.Models.Books;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BookStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private IBookService _bookService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly ILogger<BooksController> _logger;

        public BooksController(
            IBookService bookService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            ILogger<BooksController> logger
        )
        {
            _bookService = bookService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateModel model)
        {
            var book = _mapper.Map<Book>(model);

            try
            {
                await _bookService.Create(book);
                _logger.LogInformation("New book Created");
                return Ok();
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "New book exception ");
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();
            var model = _mapper.Map<IList<BookModel>>(books);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetById(id);
            var model = _mapper.Map<BookModel>(book);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateModel model)
        {
            var book = _mapper.Map<Book>(model);
            book.Id = id;

            try
            {
                await _bookService.Update(book);
                _logger.LogInformation("Book Updated");
                return Ok();
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Update book exception ");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);
            return Ok();
        }
    }
}
