using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class BookController(IGenericService<Book> bookService)
{
    [HttpGet]
    public async Task<Response<List<Book>>> GetAll()
    {
        var response = await bookService.GetAll();
        return response;
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<Book>> GetGroup(int id)
    {
        var response = await bookService.GetById(id);
        return response;
    }
    
    [HttpPost]
    public async Task<Response<bool>> Create(Book book)
    {
        var response = await bookService.Create(book);
        return response;
    }
    
    [HttpPut]
    public async Task<Response<bool>> Update(Book book)
    {
        var response = await bookService.Update(book);
        return response;
    }

    [HttpDelete] 
    public async Task<Response<bool>> Delete(int id)
    {
        var response = await bookService.Delete(id);
        return response;
    }
}