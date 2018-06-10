using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Preferences.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly IPreferenceService _preferenceService;

        public PreferencesController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }

        [HttpGet]
        public IReadOnlyDictionary<string, string> GetAll()
        {
            return _preferenceService.AllEntries;
        }

        [HttpGet("{key}")]
        public string GetValue(string key)
        {
            return _preferenceService.Get(key, "");
        }

        [HttpPost("{key}")]
        public void SetValue(string key, [FromBody] string value)
        {
            _preferenceService.Set(key, value);
        }
    }
}
