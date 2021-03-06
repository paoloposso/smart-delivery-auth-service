
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities
{
    internal abstract class BaseEntity
    {
        [BsonId()]
        public ObjectId Id { get; set; }
    }
}