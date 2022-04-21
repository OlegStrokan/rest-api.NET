using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using mvc.NET.Models;

namespace mvc.NET.Services.Users
{
    public class UsersService : IUsersService
    {
        private const string databasename = "catalog";
        private const string collectionName = "users";

        private readonly IMongoCollection<User> usersCollections;

        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;

        public UsersService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databasename);
            usersCollections = database.GetCollection<User>(collectionName);
        }

        public async Task CreateUserAsync(User user)
        {
            await usersCollections.InsertOneAsync(user);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.Id, id);
            return await usersCollections.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await usersCollections.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = filterBuilder.Eq(existingUser => existingUser.Id, user.Id);
             await usersCollections.ReplaceOneAsync(filter, user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.Id, id);
            await usersCollections.DeleteOneAsync(filter);
        }
    }
}