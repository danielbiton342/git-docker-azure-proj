using BackendAPI.DAL;
using BackendAPI.Model;
using BackendAPI.Repository;
using Microsoft.AspNetCore.Identity;



namespace BackendAPI.Logic
{
    public class LogicService
    {
        private readonly RepositoryService _repositoryService;
        private readonly IPasswordHasher<User> _passwordHasher;
        public LogicService(RepositoryService repositoryService, IPasswordHasher<User> passwordHasher)
        {
            _repositoryService = repositoryService;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> UserLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            var checkIfUserExist = await _repositoryService.IsUserExist(username);
            if(checkIfUserExist == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(password) && _passwordHasher.VerifyHashedPassword(checkIfUserExist, checkIfUserExist.Password, password) == PasswordVerificationResult.Success)
            {
                return true;
            }
            return false;
            
        }

        public async Task<bool> UserRegister(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            var checkIfUserExist = await _repositoryService.IsUserExist(username);
            if (checkIfUserExist == null)
            {
                var user = new User()
                {
                    Username = username,
                    Password = _passwordHasher.HashPassword(new User(), password)
                };
                await _repositoryService.AddUser(user);
                return true;
            }
            return false;
            
        }
    }
}
