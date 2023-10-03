using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtClub.AdminAPI.Models
{
    public record struct Exercise
    {
        [BsonElement("id")]
        public string Id { get; init; }

        [BsonElement("title")]
        public string Title { get; init; }

        [BsonElement("description")]
        public string Description { get; init; }

        [BsonElement("pictureUrl")]
        public string PictureUrl { get; init; }
    }
}
