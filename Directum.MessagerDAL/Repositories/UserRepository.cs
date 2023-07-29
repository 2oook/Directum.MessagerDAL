namespace Directum.MessagerDAL.Repositories
{
    using Microsoft.Data.SqlClient;
    using Dapper;

    using Directum.MessagerDAL.Configuration;
    using Directum.MessagerDAL.Repositories.Interfaces;
    using Directum.MessagerDAL.Models;

    public class UserRepository : IUserRepository
    {  
        private readonly string _userTableName = "[dbo].[User]";
        private readonly IConfig _config;

        public UserRepository(IConfig config)
        {
            _config = config;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM {_userTableName} WHERE Id = @Id";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
            }
        }

        public async Task<User> GetByNameAsync(string name)
        {
            Guard.IsNullOrEmpty(name);

            var sql = $"SELECT * FROM {_userTableName} WHERE Name = @Name";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Name = name });
            }
        }

        public async Task<IReadOnlyList<User>> GetAllByNameAsync(string name)
        {
            Guard.IsNullOrEmpty(name);

            var sql = $"SELECT * FROM {_userTableName} WHERE Name LIKE @Name";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<User>(sql, new { Name = name });
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(User entity)
        {
            Guard.IsNull(entity.Name);
            Guard.IsNull(entity.Password);

            var sql = $"INSERT INTO {_userTableName} (Name,Password,State) VALUES (@Name,@Password,@State); SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                return await connection.QuerySingleAsync<int>(sql, entity);
            }
        }

        public async Task UpdateAsync(User entity)
        {
            Guard.IsNull(entity.Name);
            Guard.IsNull(entity.Password);

            var sql = $"UPDATE {_userTableName} SET Name = @Name, Password = @Password, State = @State  WHERE Id = @Id";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(sql, entity);
            }
        }
    }
}