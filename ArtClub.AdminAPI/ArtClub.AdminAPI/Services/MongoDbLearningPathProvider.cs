using ArtClub.AdminAPI.Models;
using MongoDB.Driver;

namespace ArtClub.AdminAPI.Services
{
    public class MongoDbLearningPathProvider : ILearningPathProvider
    {
        private IMongoCollection<LearningPath> _learningPathCollection;

        public MongoDbLearningPathProvider(IMongoDatabase database, string collectionName)
        {
            _learningPathCollection = database.GetCollection<LearningPath>(collectionName);
        }

        public IList<LearningPathView> GetLearningPaths()
        {
            var filter = Builders<LearningPath>.Filter.Eq(p => p.Enabled, true);
            var projection = Builders<LearningPath>.Projection.Include(p => p.LearningPathId).Include(p => p.Language);
            var learningPathes = _learningPathCollection.Aggregate().Match(filter).Group(lp => lp.LearningPathId, lp => new LearningPathView()
            {
                Id = lp.Key,
                Languages = lp.Select(l=> l.Language).ToList()
            }).ToList();

            return learningPathes;
        }

        public IList<LearningPath> GetLearningPathTranslations(string id)
        {
            var filter = Builders<LearningPath>.Filter.Eq(p => p.LearningPathId, id)
                & Builders<LearningPath>.Filter.Eq(p => p.Enabled, true);
            var learningPath = _learningPathCollection.Find(filter).ToList();

            return learningPath;
        }
    }
}
