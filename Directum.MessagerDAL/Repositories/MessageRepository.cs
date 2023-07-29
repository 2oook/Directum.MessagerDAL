namespace Directum.MessagerDAL.Repositories
{
    using Microsoft.Data.SqlClient;
    using Dapper;

    using Directum.MessagerDAL.Configuration;
    using Directum.MessagerDAL.Repositories.Interfaces;
    using Directum.MessagerDAL.Models;

    public class MessageRepository : IMessageRepository
    {
        private readonly string _messageTableName = "[dbo].[Message]";
        private readonly IConfig _config;

        public MessageRepository(IConfig config)
        {
            _config = config;
        }

        public async Task<IReadOnlyList<Message>> GetAllByIdAsync(int userId)
        {
            var sql = $"SELECT * FROM {_messageTableName} WHERE UserId = @UserId";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<Message>(sql, new { UserId = userId });
                return result.ToList();
            }
        }

        public async Task<Message> GetBySubstringAsync(int userId, string substring)
        {
            Guard.IsNull(substring);

            var sql = $"SELECT * FROM {_messageTableName} WHERE UserId = @UserId AND Content LIKE + '%' +  @Substring + '%'";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                return await connection.QueryFirstOrDefaultAsync<Message>(sql, new { UserId = userId, Substring = substring });
            }
        }

        public async Task AddAsync(Message entity)
        {
            Guard.IsNullOrEmpty(entity.Content);

            var sql = $"INSERT INTO {_messageTableName} (UserId,ContactId,SendTime,Content) VALUES (@UserId,@ContactId,@SendTime,@Content)";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(sql, entity);
            }
        }
    }
}