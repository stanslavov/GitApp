using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void Create(string name, string userId, bool isPublic);

        IEnumerable<RepositoryViewModel> GetAll();

        string GetNameById(string id);

        IEnumerable<CommitViewModel> GetAllNamesWithCommits(string id);
    }
}
