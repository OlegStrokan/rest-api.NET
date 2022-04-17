using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using mvc.NET.Models;

namespace mvc.NET.Repositories
{

    public class MongoDbItemsRepository : IItemsRepository
    {

        private const string databasename = "catalog";
        private const string collectionName = "items";

        // создаем коллекцию, которую инициализируем в конструкторе
        private readonly IMongoCollection<Item> itemsCollection;
        
        
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databasename);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }
    }
}