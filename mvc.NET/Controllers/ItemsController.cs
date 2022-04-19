using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.NET.Dtos;
using mvc.NET.Models;
using mvc.NET.Repositories;

namespace mvc.NET.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        // dependency injections
        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        { 
            // select - аналогично как в sql
            return (await repository.GetItemsAsync())
                .Select(item => item.AsDto());
        }

        // GET /items/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto dto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                CreateDate = DateTimeOffset.UtcNow,
            };
            await repository.CreateItemAsync(item);

            return CreatedAtAction("GetItem", new {id = item.Id}, item.AsDto());
        }


        [HttpPut("{id}")]
        // ActionResult без дженерик типа = void;
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto dto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            // with
            Item updatedItem = existingItem with
            {
                Name = dto.Name,
                Price = dto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }


            await repository.DeleteItemAsync(id);
            
            return NoContent();
        }
    }
}