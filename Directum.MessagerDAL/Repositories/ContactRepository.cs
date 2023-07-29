namespace Directum.MessagerDAL.Repositories
{
    using Microsoft.Data.SqlClient;
    using Dapper;

    using Directum.MessagerDAL.Configuration;
    using Directum.MessagerDAL.Repositories.Interfaces;
    using Directum.MessagerDAL.Models;

    public class ContactRepository : IContactRepository
    {
        private readonly string _contactTableName = "[dbo].[Contact]";
        private readonly string _userTableName = "[dbo].[User]";
        private readonly IConfig _config;

        public ContactRepository(IConfig config)
        {
            _config = config;
        }

        public async Task<User> GetByUserIdAndContactIdAsync(int userId, int contactId)
        {
            var sql = $"SELECT u.Id, u.Name, u.Password, u.State FROM {_contactTableName} as c JOIN {_userTableName} as u on c.ContactId = u.Id WHERE UserId = @UserId AND ContactId = @ContactId";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                return await connection.QuerySingleAsync<User>(sql, new { UserId = userId, ContactId = contactId });
            }
        }

        public async Task<IReadOnlyCollection<User>> GetAllByIdAsync(int userId)
        {
            var sql = $"SELECT u.Id, u.Name, u.Password, u.State FROM {_contactTableName} as c JOIN {_userTableName} as u on c.ContactId = u.Id WHERE UserId = @UserId";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<User>(sql, new { UserId = userId });
                return result.ToList();
            }
        }

        public async Task<User> GetByNameAsync(int userId, string name)
        {
            Guard.IsNullOrEmpty(name);

            var sql = $"SELECT u.Id, u.Name, u.Password, u.State FROM {_contactTableName} as c JOIN {_userTableName} as u on c.ContactId = u.Id WHERE UserId = @UserId AND u.Name = @Name";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = userId, Name = name });
            }
        }

        public async Task AddAsync(Contact entity)
        {
            var sql = $"INSERT INTO {_contactTableName} (UserId,ContactId,LastUpdateTime) VALUES (@UserId,@ContactId,@LastUpdateTime)";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task UpdateAsync(Contact entity)
        {
            var sql = $"UPDATE {_contactTableName} SET LastUpdateTime = @LastUpdateTime WHERE UserId = @UserId AND ContactId = @ContactId";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task DeleteAsync(int userId, int contactId)
        {
            var sql = $"DELETE FROM {_contactTableName} WHERE UserId = @UserId AND ContactId = @ContactId";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(sql, new { UserId = userId, ContactId = contactId });
            }
        }
    }
}