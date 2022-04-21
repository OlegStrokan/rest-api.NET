using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.NET.Dtos.User;
using mvc.NET.Models;
using mvc.NET.Repositories;
using mvc.NET.Services.Users;

namespace mvc.NET.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;


    public UsersController(IUsersService service)
    {
        this._service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        return (await _service.GetUsersAsync())
            .Select(user => user.AsUserDto());
    }

    public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
    {
        var item = await _service.GetUserAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item.AsUserDto());
    }

    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto dto)
    {
        User user = new()
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            FullName = dto.FullName,
            Age = dto.Age,
            // TODO - need to change to IList
            Posts = new List<Post>(),
            ActivationLink = Guid.NewGuid(),
        };

        await _service.CreateUserAsync(user);

        return CreatedAtAction("GetUser", new {id = user.Id}, user.AsUserDto());
    }

    public async Task<ActionResult> UpdateUser(Guid id, UpdateUserDto dto)
    {
        var existingUser = await _service.GetUserAsync(id);

        if (existingUser is null)
        {
            return NotFound();
        }

        User updatedUser = existingUser with
        {
            UserName = dto.UserName,
            FullName = dto.FullName,
            Age = dto.Age,
        };

        await _service.UpdateUserAsync(updatedUser);

        return NoContent();
    }

    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var existingUser = await _service.GetUserAsync(id);

        if (existingUser is null)
        {
            return NotFound();
        }

        await _service.DeleteUserAsync(id);
        return NoContent();
    }
}