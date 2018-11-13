using MongoDB.Bson.Serialization.Attributes;

namespace AccountService.Core.Entities
{
    public interface IEntity<TKey>
    {
        [BsonId]
        TKey Id { get; set; }
        decimal AvailableFunds { get; set; }
        decimal Balance { get; set; }
    }
}