using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        public DataContext Context { get; private set; }
        public BooksController(DataContext context)
        {
            Context = context;
        }
        [HttpGet]
        public ActionResult GetAllBooks()
        {
            return Ok(Context.Books.ToList());
        }
        [HttpPost("{id}/{name}/{author}")]
        public IActionResult CreateBook(int id, string name, string author)
        {
            var book = new Book()
            {
                Id = id,
                Name = name,
                Author = author
            };
            Context.Books.Add(book);
            Context.SaveChanges();
            return CreatedAtAction("CreateBook",book);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = Context.Books.First(a=> a.Id == id);
            Context.Remove(book);
            Context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}/{name}")]
        public IActionResult EditName(int id, string name)
        {
            var book = Context.Books.First(a=> a.Id == id);
            Context.Books.First(a=> a == book).Name = name;
            Context.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}/{name}/{author}")]
        public IActionResult Edit(int id, string name, string author)
        {
            var book = Context.Books.First(a=> a.Id == id);
            Context.Books.First(a=> a == book).Name = name;
            Context.Books.First(a=> a == book).Author = author;
            Context.SaveChanges();
            return Ok();
        }
    }
}