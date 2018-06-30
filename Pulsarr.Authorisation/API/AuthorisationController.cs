using Microsoft.AspNetCore.Mvc;
using Pulsarr.Authorisation.ServiceInterfaces;

namespace Pulsarr.Authorisation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorisationController : ControllerBase
    {
        private readonly IAuthorisationService _authorisationService;

        public AuthorisationController(IAuthorisationService authorisationService)
        {
            _authorisationService = authorisationService;
        }

        [HttpPost]
        public bool CheckAuthorisation([FromBody] UserPostData postData)
        {
            return _authorisationService.CheckAuthentication(postData.Username, postData.Password);
        }

        [HttpPut]
        public void CreateUser([FromBody] UserPostData postData)
        {
            _authorisationService.CreateUser(postData.Username, postData.Password);
        }

        [HttpDelete("{username}")]
        public void DeleteUser(string username)
        {
            _authorisationService.DeleteUser(username);
        }
    }
}
