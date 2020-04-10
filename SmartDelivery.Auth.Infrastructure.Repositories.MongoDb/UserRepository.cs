using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Repositories;
using MongoDB.Driver;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Adapters;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private IAdapter<User, UserEntity> _adapter;

        public UserRepository(string connectionString) : base(connectionString)
        {
            _adapter = new UserAdapter();
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

            var entity = collection.Find(f => f.Email == user.Email && f.Password == user.Password).FirstOrDefault();

            return _adapter.Adapt(entity);
        }

        public User Get(string id)
        {
            IMongoCollection<UserEntity> collection = _database.GetCollection<UserEntity>("users");

            var entity = collection.Find(f => f.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefault();

            return _adapter.Adapt(entity);
        }
    }
}
