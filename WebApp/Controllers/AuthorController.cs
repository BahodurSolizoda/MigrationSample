using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthorController(IGenericService<Author> autherService)
{
    [HttpGet]
    public async Task<Response<List<Author>>> GetAll()
    {
        var response = await autherService.GetAll();
        return response;
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<Author>> GetGroup(int id)
    {
        var response = await autherService.GetById(id);
        return response;
    }
    
    [HttpPost]
    public async Task<Response<bool>> Create(Author author)
    {
        var response = await autherService.Create(author);
        return response;
    }
    
    [HttpPut]
    public async Task<Response<bool>> Update(Author author)
    {
        var response = await autherService.Update(author);
        return response;
    }

    [HttpDelete] 
    public async Task<Response<bool>> Delete(int id)
    {
        var response = await autherService.Delete(id);
        return response;
    }
}