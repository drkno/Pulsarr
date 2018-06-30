using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Pulsarr.Authorisation.Model;
using Pulsarr.Authorisation.ServiceInterfaces;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Authorisation
{
    public class AuthorisationService : IAuthorisationService
    {
        private const int SaltLength = 64;
        private const int HashIterations = 1000;
        private const int HashBytes = 64;

        private readonly IPreferenceService _preferenceService;

        public AuthorisationService(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        public bool CheckAuthentication(string username, string password)
        {
            if (_preferenceService.Get("authorisation.developerbypass", false))
            {
                return false;
            }

            try
            {
                var users = _preferenceService.GetObjectArray<User>("authorisation.users");
                if (users.Length == 0)
                {
                    return true;
                }
                var user = users.First(u => u.Username.ToLower().Trim() == username.ToLower().Trim());
                return user.PasswordHash == HashPassword(password, user.Salt);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateSalt()
        {
            var salt = new byte[SaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Encoding.UTF8.GetString(salt);
        }

        private string HashPassword(string password, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), HashIterations,
                HashAlgorithmName.SHA512) {IterationCount = HashIterations};
            return Encoding.UTF8.GetString(pbkdf2.GetBytes(HashBytes));
        }

        public void CreateUser(string username, string password)
        {
            var users = _preferenceService.GetObjectArray<User>("authorisation.users").ToList();
            if (users.Any(u => u.Username.ToLower().Trim() == username.ToLower().Trim()))
            {
                throw new InvalidOperationException("User already exists");
            }

            var salt = GenerateSalt();
            users.Add(new User(username, HashPassword(password, salt), salt));
            _preferenceService.SetObjectArray("authorisation.users", users.ToArray());
        }

        public void DeleteUser(string username)
        {
            var users = _preferenceService.GetObjectArray<User>("authorisation.users").ToList();
            users.RemoveAt(users.FindIndex(u => username.ToLower().Trim() == u.Username.ToLower().Trim()));
            _preferenceService.SetObjectArray("authorisation.users", users.ToArray());
        }
    }
}
