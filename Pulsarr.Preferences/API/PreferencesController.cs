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

        [HttpPost]
        public void SetValue([FromBody] Dictionary<string, string> value)
        {
            foreach (var key in value.Keys) {
                _preferenceService.Set(key, value[key]);
            }
        }

        [HttpDelete]
        public void DeletePreference([FromBody] string[] preferences)
        {
            foreach (var preference in preferences)
            {
                _preferenceService.Set<string>(preference, null);
            }
        }
    }
}
