using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Adapters
{
    internal class UserAdapter : IAdapter<User, UserEntity>
    {
        public UserEntity Adapt(User model)
        {
            var entity = new UserEntity {
                Document = model.Document,
                Email = model.Email,
                FullName = model.FullName,
                Password = model.Password
            };

            if (!string.IsNullOrEmpty(model.Id))
                entity.Id = MongoDB.Bson.ObjectId.Parse(model.Id);

            return entity;
        }
    }
}