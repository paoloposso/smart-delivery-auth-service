using MongoDB.Driver;
using SmartDelivery.Auth.Domain.Model;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb
{
    public class BaseRepository
    {
        protected IMongoDatabase _database;

        public BaseRepository(AppSettings appSettings)
        {
            var mongoUrl = new MongoUrl(appSettings.MongoDbCnnString);
            _database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }
    }
}