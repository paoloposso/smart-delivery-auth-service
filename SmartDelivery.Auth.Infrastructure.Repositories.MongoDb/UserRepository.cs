using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Repositories;
using MongoDB.Driver;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Adapters;
using MongoDB.Bson;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private IAdapter<User, UserEntity> _adapter;
        IMongoCollection<UserEntity> _collection;

        public UserRepository(string connectionString) : base(connectionString)
        {
            _adapter = new UserAdapter();

            _collection = _database.GetCollection<UserEntity>("users");

            CreateIndexes();
        }

        private async void CreateIndexes()
        {
            var indexOptions = new CreateIndexOptions { Unique = true };
            var keys = Builders<UserEntity>.IndexKeys.Ascending("email");
            var createIndexModel = new CreateIndexModel<UserEntity>(keys, indexOptions);

            await _collection.Indexes.CreateOneAsync(createIndexModel);
        }

        public void Insert(User model)
        {
            var entity = _adapter.Adapt(model);

            _collection.InsertOne(entity);

            model.SetId(entity.Id.ToString());
        }

        public User Get(User user)
        {
            _collection = _database.GetCollection<UserEntity>("users");

            var entity = _collection.Find(f => f.Email == user.Email && f.Password == user.Password).FirstOrDefault();

            return _adapter.Adapt(entity);
        }

        public User Get(string id)
        {
            _collection = _database.GetCollection<UserEntity>("users");

            var entity = _collection.Find(f => f.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefault();

            return _adapter.Adapt(entity);
        }
    }
}
