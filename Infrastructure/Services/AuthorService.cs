using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class AuthorService(DapperContext context):IGenericService<Author>
{
    public async Task<Response<List<Author>>> GetAll()
    {
        const string sql = "select * from Authors";
        var author=await context.Connection().QueryAsync<Author>(sql);
        return new Response<List<Author>>(author.ToList());
    }

    public async Task<Response<Author>> GetById(int id)
    {
        const string sql = "select * from Authors where Id = @Id";
        var author = await context.Connection().QueryFirstAsync<Author>(sql, new { Id = id });
        return author == null
            ? new Response<Author>(HttpStatusCode.NotFound, "Author not found")
            : new Response<Author>(author);
    }

    public async Task<Response<bool>> Create(Author author)
    {
        const string cmd = "insert into Authors (authorName, country) values (@authorName, @country)";
        var response = await context.Connection().ExecuteAsync(cmd, author);
        return response == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal server error") 
            : new Response<bool>(HttpStatusCode.Created, "Author successfully created");
    }

    public async Task<Response<bool>> Update(Author author)
    {
        var existingAuthor = await GetById(author.AuthorId);
        if (existingAuthor.Data == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Author not found");
        }
        const string cmd = "update Authors set authorName = @authorName, country = @country where id = @Id";
        var response = await context.Connection().QueryAsync(cmd, author);
        return response == null 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal server error") 
            : new Response<bool>(HttpStatusCode.OK, "Author successfully updated");
    }

    public async Task<Response<bool>> Delete(int id)
    {
        var author = await GetById(id);
        if (author.Data == null)
        {
            return new Response<bool>(HttpStatusCode.NotFound, "Author not found");
        }
        
        const string cmd = "delete from Author where id = @Id";
        var response = await context.Connection().ExecuteAsync(cmd, new {Id = id});
        return response == 0 
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal server error") 
            : new Response<bool>(HttpStatusCode.OK, "Author successfully deleted");
    }
}