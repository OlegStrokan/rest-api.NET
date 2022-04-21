using System;
using System.Collections.Generic;


namespace mvc.NET.Dtos.User
{

    public record UserDto
    {
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public string FullName { get; init; }
        public int Age { get; init; }
        public Guid ActivationLink { get; init; }
        public IList<Models.Post> Posts { get; init; }
    }
}