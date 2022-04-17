using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<ItemDto> GetItems()
        {
            // select - аналогично как в sql
            return repository.GetItems().Select(item => item.AsDto());
        }

        // GET /items/id
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto dto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                CreateDate = DateTimeOffset.UtcNow,
            };
            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }


        [HttpPut("{id}")]
        // ActionResult без дженерик типа = void;
        public ActionResult UpdateItem(Guid id, UpdateItemDto dto)
        {
            var existingItem = repository.GetItem(id);

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

            repository.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }


            repository.DeleteItem(id);
            
            return NoContent();
        }
    }
}