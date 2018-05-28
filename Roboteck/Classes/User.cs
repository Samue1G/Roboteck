namespace Roboteck.Classes
{
    class User
    {
        public string _login { get; set; }
        public string _password { get; set; }
        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }
    }
}
