using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ArtClub.AdminAPI.Models
{
    public record LearningPathView
    {
        public string Id { get; init; }
        public List<string> Languages { get; init; }
    }

    public record LearningPath
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; init; }

        [BsonElement("learningPathId")]
        [JsonPropertyName("id")]
        public string LearningPathId { get; init; }

        [BsonElement("language")]
        public string Language { get; init; }

        [BsonElement("title")]
        public string Title { get; init; }

        [BsonElement("description")]
        public string Description { get; init; }

        [BsonElement("enabled")]
        public bool Enabled { get; init; }

        [BsonElement("steps")]
        public IList<Step>? Steps { get; init; }
    }
}
