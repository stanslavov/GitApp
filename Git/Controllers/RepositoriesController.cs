using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            bool isPublic = true;

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (String.IsNullOrEmpty(name) || name.Length < 5 || name.Length > 10)
            {
                return this.Error("Invalid name. Name should be between 5 and 10 characters.");
            }

            if (repositoryType == "Public")
            {
                isPublic = true;
            }
            else
            {
                isPublic = false;
            }

            var userId = this.GetUserId();
            this.repositoriesService.Create(name, userId, isPublic);
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.repositoriesService.GetAll();

            return this.View(viewModel);
        }
    }
}
