namespace Directum.MessagerDAL.Repositories.Interfaces
{
    using Directum.MessagerDAL.Models;

    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id);

        public Task<User> GetByNameAsync(string name);

        public Task<IReadOnlyList<User>> GetAllByNameAsync(string name);

        public Task<int> AddAsync(User entity);

        public Task UpdateAsync(User entity);
    }
}