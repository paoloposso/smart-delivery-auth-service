using MongoDB.Bson.Serialization.Attributes;

namespace SmartDelivery.Auth.Infrastructure.Repositories.MongoDb.Entities
{
    internal class UserEntity : BaseEntity
    {

        [BsonElement("fullName")]
        [BsonRequired()]
        public string FullName { get; set; }

        [BsonElement("document")]
        [BsonRequired()]
        public string Document { get; set; }

        [BsonElement("email")]
        [BsonRequired()]
        public string Email { get; set; }

        [BsonElement("password")]
        [BsonRequired()]
        public string Password {get; set; }
    }
}