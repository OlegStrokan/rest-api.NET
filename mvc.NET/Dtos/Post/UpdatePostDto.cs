using System.ComponentModel.DataAnnotations;

namespace mvc.NET.Dtos.Post
{

    public record UpdatePostDto
    {
        [Required]
        [StringLength(20)]
        public string Title { get; init; }
        [Required]
        [StringLength(200)]
        public string Content { get; init; }
    }
}