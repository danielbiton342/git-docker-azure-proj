using MongoDB.Bson;

namespace BackendAPI.Model
{
    public class User
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Username { get; set; } = string.Empty;

        public string Password {  get; set; } = string.Empty;
    }
}
