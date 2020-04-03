using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Adapters
{
    internal interface IAdapter<TIModel, TMongoEntity> 
        where TIModel : IModel where TMongoEntity: BaseEntity
    {
        TMongoEntity Adapt(TIModel model);
    }
}