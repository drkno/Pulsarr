namespace Pulsarr.Authorisation.ServiceInterfaces
{
    public interface IAuthorisationService
    {
        bool CheckAuthentication(string username, string password);
        void CreateUser(string username, string password);
        void DeleteUser(string username);
    }
}