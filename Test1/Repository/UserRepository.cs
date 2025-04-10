namespace Test1.Repository
{
    public class UserRepository
    {
        public UserRepository() { }
        public bool LoginUser(string userName, string password)
        {
            // Here you would typically check the credentials against a database
            // For demonstration purposes, we'll use hardcoded values
            if (userName == "Mukesh" && password == "Negi")
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
