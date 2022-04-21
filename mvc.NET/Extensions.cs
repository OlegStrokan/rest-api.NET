using mvc.NET.Dtos;
using mvc.NET.Dtos.User;
using mvc.NET.Models;

namespace mvc.NET
{

    public static class Extensions
    {
       // аргументом принимает item по контексту к которому вызван метод
        public static PostDto AsPostDto(this Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateDate = post.CreateDate
            };
        }

        public static UserDto AsUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Age = user.Age,
                ActivationLink = user.ActivationLink,
                Posts = user.Posts,
            };
        }
    }
}