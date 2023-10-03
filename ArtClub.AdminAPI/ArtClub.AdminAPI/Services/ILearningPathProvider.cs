using ArtClub.AdminAPI.Models;

namespace ArtClub.AdminAPI.Services
{
    public interface ILearningPathProvider
    {
        IList<LearningPathView> GetLearningPaths();
        IList<LearningPath> GetLearningPathTranslations(string id);
    }
}
