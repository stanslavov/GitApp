using Git.Data;
using System;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string id, string userId, string description)
        {
            var commit = new Commit
            {
                Description = description,
                CreatorId = userId,
                RepositoryId = id,
                CreatedOn = DateTime.UtcNow
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var commit = this.db.Commits.FirstOrDefault(x => x.Id == id);

            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }
    }
}
