using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using mvc.NET.Models;

namespace mvc.NET.Repositories
{

    public class PostsService : IPostsService
    {

        private const string databasename = "catalog";
        private const string collectionName = "posts";

        // создаем коллекцию, которую инициализируем в конструкторе
        private readonly IMongoCollection<Post> postsCollection;
        
        
        private readonly FilterDefinitionBuilder<Post> filterBuilder = Builders<Post>.Filter;
        

        public PostsService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databasename);
            postsCollection = database.GetCollection<Post>(collectionName);
        }
        public async Task CreatePostAsync(Post post)
        {
             await postsCollection.InsertOneAsync(post);
        }
        
        public async Task<Post> GetPostAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await postsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await postsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, post.Id);
            await postsCollection.ReplaceOneAsync(filter, post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await postsCollection.DeleteOneAsync(filter);
        }
    }
}