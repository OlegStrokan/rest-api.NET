using System;

namespace mvc.NET.Dtos
{

    public record PostDto 
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public DateTimeOffset CreateDate { get; init; }


    }

}