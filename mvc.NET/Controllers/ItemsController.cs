using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mvc.NET.Models;
using mvc.NET.Repositories;

namespace mvc.NET.Controllers
{
    
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }

        // GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return repository.GetItems();
          
        }

        // GET /items/id
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

    }
}