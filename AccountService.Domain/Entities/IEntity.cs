using MongoDB.Bson.Serialization.Attributes;

namespace AccountService.Domain.Entities
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
    }
}