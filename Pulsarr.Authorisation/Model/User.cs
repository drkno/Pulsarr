using Pulsarr.Preferences.DataModel.Preferences;

namespace Pulsarr.Authorisation.Model
{
    public class User
    {
        [Preference]
        public string Username { get; }
        [Preference]
        public string PasswordHash { get; }
        [Preference]
        public string Salt { get; }

        public User() { }

        public User(string username, string passwordHash, string salt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }
    }
}
