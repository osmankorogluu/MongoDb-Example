using BookStore.Models;
using BookStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await _bookService.Get();
        }

        [HttpGet]
        [Route("getid")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            var result = await _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.Remove(book.Id);

            return NoContent();
        }
    }
}
