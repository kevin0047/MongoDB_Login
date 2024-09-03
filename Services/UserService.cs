
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_Login.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDB_Login.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("MongoDB_Login");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            return await _users.Find(user => user.Username == username && user.Password == password).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateUserAsync(User newUser)
        {
            var existingUser = await _users.Find(user => user.Username == newUser.Username).FirstOrDefaultAsync();
            if (existingUser == null)
            {
                await _users.InsertOneAsync(newUser);
                return true;
            }
            return false;
        }
    }
}
