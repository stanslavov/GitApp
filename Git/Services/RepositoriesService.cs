using Git.Data;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string userId, bool isPublic)
        {
            var repository = new Repository
            {
                Name = name,
                IsPublic = isPublic,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            return this.db.Repositories.Select(x => new RepositoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn,
                IsPublic = x.IsPublic,
                Count = x.Commits.Count
            }).ToList();
        }

        public string GetNameById(string id)
        {
            return this.db.Repositories.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
        }

        public IEnumerable<CommitViewModel> GetAllNamesWithCommits(string userId)
        {
            return this.db.Commits.Where(x => x.CreatorId == userId).Select(x => new CommitViewModel
            {
                RepositoryName = x.Repository.Name,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                Id = x.Id
            }).ToList();
        }
    }
}
