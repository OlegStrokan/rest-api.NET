using System;
using System.Collections.Generic;
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

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databasename);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void UpdateItem(Item item)
        {
            
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}