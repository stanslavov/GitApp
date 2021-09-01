using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController(IRepositoriesService repositoriesService, ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CreateCommitInRepositoryViewModel
            {
                Id = id,
                Name = this.repositoriesService.GetNameById(id)
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string id, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (String.IsNullOrEmpty(description) || description.Length < 5)
            {
                return this.Error("Description should be at least 5 characters.");
            }

            var userId = this.GetUserId();
            this.commitsService.Create(id, userId, description);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = GetUserId();
            var viewModel = this.repositoriesService.GetAllNamesWithCommits(userId);

            return this.View(viewModel);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.commitsService.Delete(id);
            return this.Redirect("/Commits/All");
        }
    }
}
