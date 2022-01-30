using BookStore.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public async Task<List<Book>> Get()
        {
            var result = await _books.Find(book => true).ToListAsync();
            return result;
        }

        public async Task<Book> Get(string id)
        {
            return await _books.Find<Book>(book => book.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task Update(string id, Book bookIn)
        {
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);
        }

        public async Task Remove(Book bookIn)
        {
            await _books.DeleteOneAsync(book => book.Id == bookIn.Id);
        }

        public async Task Remove(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }
    }
}
