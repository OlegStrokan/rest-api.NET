using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mvc.NET.Models;

namespace mvc.NET.Repositories;

public interface IPostsService
{
    Task<Post> GetPostAsync(Guid id);

    Task<IEnumerable<Post>> GetPostsAsync();

    Task CreatePostAsync(Post post);
    Task UpdatePostAsync(Post post);
    Task DeletePostAsync(Guid id);
}