using Test1.Repository;

namespace Test1.Services
{
    public class UserService
    {
        public bool LoginUser(string userName, string password)
        {   // Here you would typically check the credentials against a database
            // For demonstration purposes, we'll use hardcoded values
            UserRepository userRepository = new UserRepository();
            var isValid = userRepository.LoginUser(userName, password);
            if (isValid)
            {
                return true; // Login successful
            }
            else
            {
                return false; // Login failed
            }
        }
    }
}
