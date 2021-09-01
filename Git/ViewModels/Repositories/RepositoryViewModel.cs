using System;

namespace Git.ViewModels.Repositories
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Count { get; set; }
    }
}
