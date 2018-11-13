using MongoDB.Bson.Serialization.Attributes;

namespace Intro.Core.Entities
{
    public class Account : IEntity<ulong>
    {
        [BsonId]
        public ulong Id { get; set; }
        public string Name { get; set; }
        public decimal AvailableFunds { get; set; } = 0m;
        public decimal Balance { get; set; } = 0m;
        public bool HasCard { get; set; } = false;
    }
}
