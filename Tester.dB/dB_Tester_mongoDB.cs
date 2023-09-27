
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.dB
{
    public class dB_Tester_mongoDB : IdB_Tester
    {

        private const string CONNECTIONSTRING = "mongodb://localhost:27017";
        private MongoClient? _client;
        private IMongoDatabase? _database;

        public void AnExample()
        {
            Initiate();
            SaveData();
        }

        private bool Initiate()
        {
            // Create a new MongoClient object using the connection string
            _client = new MongoClient(CONNECTIONSTRING);

            // Get a reference to the database
            _database = _client.GetDatabase("local");

            return true;
        }

        private void SaveData()
        {
            while (true)
            {
                //var books = _database.GetCollection<Book>("books");
                //var publishers = _database.GetCollection<Publisher>("publishers");

                //// Create a new publisher
                //var publisher = new Publisher
                //{
                //    Name = "Publisher Name",
                //    Founded = 2000,
                //    Location = "Publisher Location",
                //};

                //try
                //{
                //    publishers.InsertOneAsync(publisher);
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.ToString());
                //}
                //// Create a new book
                //var book = new Book
                //{
                //    Title = "Book Title",
                //    Author = new List<string> { "Author1", "Author2" },
                //    PublishedDate = DateTime.Now,
                //    Pages = 200,
                //    Language = "English",
                //    PublisherId = publisher.Id.ToString()
                //};

                //// Insert the book
                //books.InsertOneAsync(book);

                var collection = _database.GetCollection<BsonDocument>("books");

                var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId("65124a4eac19c49a6c2875b4"));

                var document = Task.Run(() => collection.Find(filter).FirstOrDefaultAsync());

                if (document != null)
                {
                    Console.WriteLine(document.ToString());
                }
                else
                {
                    Console.WriteLine("No document found with the provided _id.");
                }
            }
        }
    }

    internal class Book
    {
        public ObjectId Id { get; set; }

        public String Title { get; set; }
        
        public String PublisherId { get; set;}

        public int Pages { get; set;}

        public DateTime PublishedDate { get; set; }

        public String Language { get; set; }

        public List<string>? Author { get; set; }
    }

    internal class Publisher
    {
        public String Name { get; set; }

        public String Location { get; set; }

        public int Founded { get; set; }

        public ObjectId Id { get; set; }
    }


}
