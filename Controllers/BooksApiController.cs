
using BooksApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public BooksApiController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        

        [HttpGet]
        [Route("get-books-sortedlist-publisher")]
        public async Task<IActionResult> GetSortedAsync()
        {
            var sortedbooks = await _appDbContext.Books
                .OrderBy(b => b.Publisher)
                .ThenBy(b => b.AuthorLastName)
                .ThenBy(b => b.AuthorFirstName)
                .ThenBy(b => b.Title)
                .ToListAsync();

            return Ok(sortedbooks);
        }
        [HttpGet]
        [Route("get-books-list-by-author")]
        public async Task<IActionResult> GetByAuthorAsync()
        {
            var books = await _appDbContext.Books
                .OrderBy(b => b.AuthorLastName)
                .ThenBy(b => b.AuthorFirstName)
                .ThenBy(b => b.Title)                
                .ToListAsync();

            return Ok(books);
        }
        [HttpGet]
        [Route("get-books-StoredProcedure-by-publisher-author-title")]
        public async Task<IActionResult> GetSortedByPublisherAuthorTitleAsync()
        {
            var books = await _appDbContext.Books
                .FromSqlRaw("EXEC GetBooksSortedByPublisherAuthorTitle")
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet]
        [Route("get-books-StoredProcedure-by-author-title")]
        public async Task<IActionResult> GetSortedByAuthorTitleAsync()
        {
            var books = await _appDbContext.Books
                .FromSqlRaw("EXEC GetBooksSortedByAuthorTitle")
                .ToListAsync();

            return Ok(books);
        }
        [HttpGet]
        [Route("get-total-price")]
        public async Task<IActionResult> GetTotalPriceAsync()
        {
            decimal totalPrice = await _appDbContext.Books
                .SumAsync(b => b.Price);

            return Ok(new { TotalPrice = totalPrice });
        }

        [HttpGet]
        [Route("get-Mla-citations")]
        public async Task<IActionResult> GetAsync()
        {
            var books = await _appDbContext.Books
                .OrderBy(b => b.Publisher)
                .ThenBy(b => b.AuthorLastName)
                .ThenBy(b => b.AuthorFirstName)
                .ThenBy(b => b.Title)
                .ToListAsync();

            
            var response = books.Select(b => new
            {
                b.Publisher,
                b.AuthorLastName,
                b.AuthorFirstName,
                b.Title,
                b.MLACitation  
            });

            return Ok(response);
        }
        [HttpGet]
        [Route("get-chicago-citations")]
        public async Task<IActionResult> GetChicagoCitationsAsync()
        {
            var citations = await _appDbContext.Books
                .Select(b => new
                {
                    b.Publisher,
                    b.AuthorLastName,
                    b.AuthorFirstName,
                    b.Title,
                    b.Location,
                    b.YearOfPublication,
                    b.Month,
                    b.PageNumberRange,
                    b.VolumeNumber,
                    ChicagoCitation = b.ChicagoCitation 
                })
                .ToListAsync();

            return Ok(citations);
        }

    }
}