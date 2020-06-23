using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace demo
{
    class Program
    {
        private const string MongoDBConnection = "mongodb://localhost:27031/admin";

        private static IMongoClient _client = new MongoClient(MongoDBConnection);
        private static IMongoDatabase _database = _client.GetDatabase("Test");
        private static IMongoCollection<CollectionModel> _collection = _database.GetCollection<CollectionModel>("TestCollection");

        static async Task Main(string[] args)
        {
            //BsonSerializer.RegisterSerializer(typeof(DateTime), new MyDateTimeSerializer());

            await Demo();
            //Console.ReadKey();
        }

        private static async Task Demo()
        {
            CollectionModel new_item = new CollectionModel()
            {
                title = "Demo",
                content = "Demo content",
                favor = 100,
                author = new Author
                {
                    name = "WangPlus",
                    contacts = new List<Contact>(),
                },
                tag = TagEnumeration.CSharp,
                post_time = DateTime.Now,
                ignore_string = "hello",
                extra_info = new Dictionary<string, int>(),
            };

            Contact contact_item1 = new Contact()
            {
                mobile = "13800000000",
            };
            Contact contact_item2 = new Contact()
            {
                mobile = "13811111111",
            };
            new_item.author.contacts.Add(contact_item1);
            new_item.author.contacts.Add(contact_item2);

            new_item.extra_info.Add("type", 1);
            new_item.extra_info.Add("mode", 2);

            await _collection.InsertOneAsync(new_item);
            
        }
    }
}

