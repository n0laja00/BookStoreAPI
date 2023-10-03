using BookStoreAPI.Controllers;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;


namespace BooksStoreAPI.Tests
{
    public class BookStoreTests
    {
        /// <summary>
        /// Creates a temporary inMemory database to use for the sake of testing.
        /// </summary>
        /// <returns>DataContext</returns>
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Book.CountAsync() <= 0)
            {
                for (var i = 0; i < 10; i++)
                {
                    databaseContext.Book.Add(
                        new Book()
                        {
                            Title = i.ToString(),
                            Author = i.ToString(),
                            Year = i,
                            Publisher = i.ToString(),
                            Description = i.ToString(),
                        }
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        
        [Fact]
        public async void BookStoreAPI_GetBookByAuthor_ReturnsBook()
        {
            //arrange
            var author = "1";
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            var tempResultString = "";
            List<Book> bookList = new List<Book>();

            //act
            var result = testSubjectController.GetBook(author, null, null);
            tempResultString = result.Result.ToString();
            bookList = JsonConvert.DeserializeObject<List<Book>>(tempResultString);

            //assert

            bookList.Should().NotBeNull().And.HaveCount(1);
            bookList[0].Author.Should().Be(author);
            bookList.Should().BeOfType<List<Book>>();
        }
        
        [Fact]
        public async void BookStoreAPI_GetBookByYear_ReturnsBook()
        {
            //arrange
            var year = 2;
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            var tempResultString = "";
            List<Book> bookList = new List<Book>();

            //act
            var result = testSubjectController.GetBook(null, year, null);
            tempResultString = result.Result.ToString();
            bookList = JsonConvert.DeserializeObject<List<Book>>(tempResultString);

            //assert

            bookList.Should().NotBeNull().And.HaveCount(1);
            bookList[0].Year.Should().Be(year);
            bookList.Should().BeOfType<List<Book>>();
        }
        
        [Fact]
        public async void BookStoreAPI_GetBookByPublisher_ReturnsBook()
        {
            //arrange
            var publisher = "3";
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            var tempResultString = "";
            List<Book> bookList = new List<Book>();

            //act
            var result = testSubjectController.GetBook(null, null, publisher);
            tempResultString = result.Result.ToString();
            bookList = JsonConvert.DeserializeObject<List<Book>>(tempResultString);

            //assert

            bookList.Should().NotBeNull().And.HaveCount(1);
            bookList[0].Publisher.Should().Be(publisher);
            bookList.Should().BeOfType<List<Book>>();
        }
        
        [Fact]
        public async void BookStoreAPI_GetBook_ReturnsErrorBecauseEmptyStringAuthor()
        {
            //arrange
            var author = "";
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            var tempResultString = "";

            //act
            var result = testSubjectController.GetBook(author, null, null);

            //assert
            result.Exception.Should().NotBeNull();
        }

        [Fact]
        public async void BookStoreAPI_GetBook_ReturnsErrorBecauseEmptyStringPublisher()
        {
            //arrange
            var publisher = "";
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            var tempResultString = "";

            //act
            var result = testSubjectController.GetBook(publisher, null, null);

            //assert
            result.Exception.Should().NotBeNull();
        }

        [Fact]
        public async void BookStoreAPI_GetBook_ReturnsAllBooks()
        {
            //arrange
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            var tempResultString = "";
            List<Book> bookList = new List<Book>();


            //act
            var result = testSubjectController.GetBook(null, null, null);
            tempResultString = result.Result.ToString();
            bookList = JsonConvert.DeserializeObject<List<Book>>(tempResultString);


            //assert
            bookList.Should().NotBeNull().And.HaveCount(10);
            bookList.Should().BeOfType<List<Book>>();
        }

        [Fact]
        public async void BookStoreAPI_DeleteBook_ReturnContainsNoContent()
        {
            //arrange
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            int deleteBookId = 2;
            string tempResultString;
            Object resultObject;


            //act
            var result = testSubjectController.DeleteBook(deleteBookId);


            //assert
            result.Exception.Should().NotBeNull();
        }

        [Fact]
        public async void BookStoreAPI_Post_ReturnsId()
        {
            //arrange
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            PostRequestResponseId responseBookId;
            Book book = new Book()
            {
                Id = 11,
                Title = "BookStoreAPI Post ReturnsId",
                Author = "BookStoreAPI Post ReturnsId",
                Year = 11,
                Publisher = "BookStoreAPI Post ReturnsId",
                Description = "BookStoreAPI Post ReturnsId"
            };

            //act
            var result = await testSubjectController.PostBook(book);
            responseBookId = JsonConvert.DeserializeObject<PostRequestResponseId>(result.Value.ToString());

            //assert
            responseBookId.Id.Should().NotBeNull();
            responseBookId.Should().BeOfType<PostRequestResponseId>();
            responseBookId.Id.Should().Be(11);

            
        }

        [Fact]
        public async void BookStoreAPI_PostWithoutDescription_ReturnsId()
        {
            //arrange
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            PostRequestResponseId responseBookId;
            Book book = new Book()
            {
                Id = 12,
                Title = "BookStoreAPI Post ReturnsId",
                Author = "BookStoreAPI Post ReturnsId",
                Year = 12,
                Publisher = "BookStoreAPI Post ReturnsId",
                Description = null
            };

            //act
            var result = await testSubjectController.PostBook(book);
            responseBookId = JsonConvert.DeserializeObject<PostRequestResponseId>(result.Value.ToString());

            //assert
            responseBookId.Id.Should().NotBeNull();
            responseBookId.Should().BeOfType<PostRequestResponseId>();
            responseBookId.Id.Should().Be(12);

            
        }

        [Fact]
        public async void BookStoreAPI_PostWithoutPublisher_ReturnsId()
        {
            //arrange
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            PostRequestResponseId responseBookId;
            Book book = new Book()
            {
                Id = 13,
                Title = "BookStoreAPI Post ReturnsId",
                Author = "BookStoreAPI Post ReturnsId",
                Year = 13,
                Publisher = null,
                Description = "BookStoreAPI Post ReturnsId"
            };

            //act
            var result = await testSubjectController.PostBook(book);
            responseBookId = JsonConvert.DeserializeObject<PostRequestResponseId>(result.Value.ToString());

            //assert
            responseBookId.Id.Should().NotBeNull();
            responseBookId.Should().BeOfType<PostRequestResponseId>();
            responseBookId.Id.Should().Be(13);

            
        }

        [Fact]
        public async void BookStoreAPI_PostWithoutDescriptionAndPublisher_ReturnsId()
        {
            //arrange
            var dbContext = await GetDatabaseContext();
            var testSubjectController = new BooksController(dbContext);
            PostRequestResponseId responseBookId;
            Book book = new Book()
            {
                Id = 14,
                Title = "BookStoreAPI Post ReturnsId",
                Author = "BookStoreAPI Post ReturnsId",
                Year = 14,
                Publisher = null,
                Description = null
            };

            //act
            var result = await testSubjectController.PostBook(book);
            responseBookId = JsonConvert.DeserializeObject<PostRequestResponseId>(result.Value.ToString());

            //assert
            responseBookId.Id.Should().NotBeNull();
            responseBookId.Should().BeOfType<PostRequestResponseId>();
            responseBookId.Id.Should().Be(14);

            
        }
    }
}
