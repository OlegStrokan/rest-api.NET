using mvc.NET.Dtos;
using mvc.NET.Models;

namespace mvc.NET
{

    public static class Extensions
    {
       // аргументом принимает item по контексту к которому вызван метод
        public static PostDto AsDto(this Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate
            };
        }
    }
}