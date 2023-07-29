namespace Directum.MessagerDAL.Repositories.Interfaces
{
    using Directum.MessagerDAL.Models;

    public interface IMessageRepository
    {
        public Task<IReadOnlyList<Message>> GetAllByIdAsync(int userId);

        public Task<Message> GetBySubstringAsync(int userId, string substring);

        public Task AddAsync(Message entity);
    }
}