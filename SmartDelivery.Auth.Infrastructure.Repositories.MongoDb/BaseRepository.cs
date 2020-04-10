using MongoDB.Driver;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb
{
    public class BaseRepository
    {
        protected IMongoDatabase _database;

        public BaseRepository(string connectionString)
        {
            var mongoUrl = new MongoUrl(connectionString);
            _database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }
    }
}