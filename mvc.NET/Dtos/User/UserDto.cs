using System;
using System.Collections.Generic;


namespace mvc.NET.Dtos.User
{

    public record UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string ActivationLink { get; set; }
        public IList<Models.Post> Posts { get; set; }
    }
}