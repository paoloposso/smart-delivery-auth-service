using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Repositories;
using MongoDB.Driver;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Adapters;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb
{
    public class UserRepository : IUserRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IAdapter<User, UserEntity> _adapter;

        public UserRepository()
        {
            _adapter = new UserAdapter();
            _client = new MongoClient("mongodb://192.168.99.100:27017");
            _database = _client.GetDatabase("SmartDeliveryAuthDev");
        }

        public void Insert(User model)
        {
            var entity = _adapter.Adapt(model);

            IMongoCollection<UserEntity> collection = _database.GetCollection<UserEntity>("users");

            collection.InsertOne(entity);

            model.SetId(entity.Id.ToString());
        }

        public User Get(User user)
        {
            IMongoCollection<UserEntity> collection = _database.GetCollection<UserEntity>("users");

            var entity = collection.Find(f => f.Email == user.Email).First();

            return _adapter.Adapt(entity);
        }
    }
}
