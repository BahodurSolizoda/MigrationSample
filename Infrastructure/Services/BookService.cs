using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BookService(DapperContext context):IGenericService<Book>
{
    public async Task<Response<List<Book>>> GetAll()
    {
        const string sql = "select * from Books";
        var books=await context.Connection().QueryAsync<Book>(sql);
        return new Response<List<Book>>(books.ToList());
    }

    public async Task<Response<Book>> GetById(int id)
    {
        const string sql = "select * from Books where Id = @Id";
        var books = await context.Connection().QueryFirstAsync<Book>(sql, new { Id = id });
        return books == null
            ? new Response<Book>(HttpStatusCode.NotFound, "Book not found")
            : new Response<Book>(books);
    }

    public async Task<Response<bool>> Create(Book book)
    {
        const string cmd = "insert into Books (title, authorId, publishedYear, genre, isAvailable) values (@title, @authorId, @publishedYear, @genre, @isAvailable)";
        var response = await context.Connection().ExecuteAsync(cmd, book);
        return response == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal server error") 
            : new Response<bool>(HttpStatusCode.Created, "Author successfully created");
    }

    public async Task<Response<bool>> Update(Book book)
    {
        var existingBook = await GetById(book.BookId);
        if (existingBook.Data == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Book not found");
        }
        const string cmd = "update groups set title = @title, authorId = @authorId, publishedYear=@publishedYear, genre=@genre, isAvailable=@isAvailable where id = @Id";
        var response = await context.Connection().QueryAsync(cmd, book);
        return response == null 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal server error") 
            : new Response<bool>(HttpStatusCode.OK, "Book successfully updated");
    }

    public async Task<Response<bool>> Delete(int id)
    {
        var books = await GetById(id);
        if (books.Data == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Book not found");
        }
        
        const string cmd = "delete from Books where id = @Id";
        var response = await context.Connection().ExecuteAsync(cmd, new {Id = id});
        return response == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal server error") 
            : new Response<bool>(HttpStatusCode.OK, "Book successfully deleted");
    }
}