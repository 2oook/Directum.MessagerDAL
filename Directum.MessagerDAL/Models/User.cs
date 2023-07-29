namespace Directum.MessagerDAL.Models
{
    using Directum.MessagerDAL.Enum;
    using System.Text.Json.Serialization;

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserStateEnum State { get; set; }
    }
}