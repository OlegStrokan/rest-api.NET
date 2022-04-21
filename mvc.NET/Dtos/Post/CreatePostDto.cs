using System.ComponentModel.DataAnnotations;

namespace mvc.NET.Dtos.Post
{

    public record CreatePostDto
    {
        [Required]
        [MaxLength(20), MinLength(5)]
        public string Title { get; init; }
        [Required]
        [MaxLength(200), MinLength(10)]
        public string Content { get; init; }
    }
}