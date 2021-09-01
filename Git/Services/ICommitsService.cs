namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string problemId, string userId, string code);

        void Delete(string id);
    }
}
