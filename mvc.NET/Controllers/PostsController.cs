using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.NET.Dtos;
using mvc.NET.Dtos.Post;
using mvc.NET.Models;
using mvc.NET.Repositories;

namespace mvc.NET.Controllers
{
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
        public async Task<IEnumerable<PostDto>> GetItemsAsync()
        { 
            // select - аналогично как в sql
            return (await _service.GetPostsAsync())
                .Select(item => item.AsDto());
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetItemAsync(Guid id)
        {
            var item = await _service.GetPostAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreateItemAsync(CreatePostDto dto)
        {
            Post post = new()
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                CreateDate = DateTimeOffset.UtcNow,
            };
            await _service.CreatePostAsync(post);

            return CreatedAtAction("GetItem", new {id = post.Id}, post.AsDto());
        }


        [HttpPut("{id}")]
        // ActionResult без дженерик типа = void;
        public async Task<ActionResult> UpdateItem(Guid id, UpdatePostDto dto)
        {
            var existingItem = await _service.GetPostAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            // with
            Post updatedPost = existingItem with
            {
                Title = dto.Title,
                Content = dto.Content
            };

            await _service.UpdatePostAsync(updatedPost);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await _service.GetPostAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }


            await _service.DeletePostAsync(id);
            
            return NoContent();
        }
    }
}