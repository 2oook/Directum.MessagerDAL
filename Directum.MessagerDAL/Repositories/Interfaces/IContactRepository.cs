namespace Directum.MessagerDAL.Repositories.Interfaces
{
    using Directum.MessagerDAL.Models;

    public interface IContactRepository
    {
        public Task<User> GetByUserIdAndContactIdAsync(int userId, int contactId);

        public Task<IReadOnlyCollection<User>> GetAllByIdAsync(int userId);

        public Task<User> GetByNameAsync(int userId, string name);

        public Task AddAsync(Contact entity);

        public Task UpdateAsync(Contact entity);

        public Task DeleteAsync(int userId, int contactId);
    }
}