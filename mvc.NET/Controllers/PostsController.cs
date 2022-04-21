using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.NET.Dtos;
using mvc.NET.Dtos.Post;
using mvc.NET.Models;
using mvc.NET.Repositories;

namespace mvc.NET.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly IPostsService _service;


    public PostsController(IPostsService service)
    {
        this._service = service;
    }


    [HttpGet]
    public async Task<IEnumerable<PostDto>> GetPostsAsync()
    {
        // select - аналогично как в sql
        return (await _service.GetPostsAsync())
            .Select(item => item.AsPostDto());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetPostAsync(Guid id)
    {
        var item = await _service.GetPostAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item.AsPostDto());
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePostAsync(CreatePostDto dto)
    {
        Post post = new()
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            CreateDate = DateTimeOffset.UtcNow,
        };
        await _service.CreatePostAsync(post);

        return CreatedAtAction("GetPost", new {id = post.Id}, post.AsPostDto());
    }


    [HttpPut("{id}")]
    // ActionResult без дженерик типа = void;
    public async Task<ActionResult> UpdatePost(Guid id, UpdatePostDto dto)
    {
        var existingPost = await _service.GetPostAsync(id);

        if (existingPost is null)
        {
            return NotFound();
        }

        // with
        Post updatedPost = existingPost with
        {
            Title = dto.Title,
            Content = dto.Content
        };

        await _service.UpdatePostAsync(updatedPost);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(Guid id)
    {
        var existingPost = await _service.GetPostAsync(id);

        if (existingPost is null)
        {
            return NotFound();
        }


        await _service.DeletePostAsync(id);

        return NoContent();
    }
}