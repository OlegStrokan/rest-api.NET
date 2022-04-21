using System;

namespace mvc.NET.Models
{

    public record Post
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public DateTimeOffset CreateDate { get; init; }
        
    }
} 