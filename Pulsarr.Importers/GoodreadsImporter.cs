
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Goodreads;
using Pulsarr.Preferences;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Importers
{
    public class GoodreadsImporter : IImporter
    {
        private readonly IPreferenceService _preferenceService;
        public string SourceId { get; } = "GoodReads";
        public bool Enabled => _preferenceService.Get("goodreads.enabled", true);
        private readonly IGoodreadsClient _client;
        readonly HttpClient client = new HttpClient();

        public GoodreadsImporter(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
            _client = GoodreadsClient.Create(
                preferenceService.Get("goodreads.apikey", "ckvsiSDsuqh7omh74ZZ6Q"), // todo: this is the lazylibrarian key...
                preferenceService.Get("goodreads.apisecret", "")
            );
        }
        public async Task Import(string url)
        {
            var html = await client.GetStringAsync(url);
            var idRegex = new Regex(@"http:\/\/www.goodreads.com\/book\/show\/([0-9]+).\w+");

            var matches = idRegex.Matches(html);

            foreach (var match in matches) {
                Console.WriteLine(match);
            }
            return;
        }
    }
}