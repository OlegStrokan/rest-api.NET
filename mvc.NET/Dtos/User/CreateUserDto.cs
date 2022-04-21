using System;
using System.ComponentModel.DataAnnotations;

namespace mvc.NET.Dtos.User
{

    public record CreateUserDto
    {
        [Required]
        [MaxLength(20), MinLength(5)]
        public string UserName { get; init; }
        [Required]
        [MaxLength(20), MinLength(5)]
        public string FullName { get; init; }
        [Required]
        [Range(0, 125)]
        public int Age { get; init; }
    }
}