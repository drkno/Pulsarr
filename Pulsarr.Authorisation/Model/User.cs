using Pulsarr.Preferences.DataModel.Preferences;

namespace Pulsarr.Authorisation.Model
{
    public class User
    {
        [Preference]
        public string Username { get; set; }
        [Preference]
        public string PasswordHash { get; set; }
        [Preference]
        public string Salt { get; set; }
        [Preference]
        public string Roles { get; set; }

        public User() { }

        public User(string username, string passwordHash, string salt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }
    }
}
