using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Pulsarr.Search.Client.Newznab.Model;
using Pulsarr.Search.Manager.Model;
using Pulsarr.Search.ServiceInterfaces;

namespace Pulsarr.Search.Client.Newznab
{
    public class NewzNabClient : ISearchProvider
    {
        private string ApiKey { get; }
        private Uri ApiUrl { get; }
        private List<int> Categories { get; set; }

        public NewzNabClient(IndexerPreferences add)
        {
            Categories = new List<int>();
            ApiKey = add.ApiKey;

            var builder = new UriBuilder(add.Host)
            {
                Scheme = add.UseTls
                    ? Uri.UriSchemeHttps
                    : Uri.UriSchemeHttp
            };
            if (!builder.Path.EndsWith(add.Resource))
            {
                builder.Path = builder.Path.TrimEnd('/') + '/' + add.Resource.TrimStart('/');
            }
            builder.Query = "";
            ApiUrl = builder.Uri;
        }
        
        private async Task<List<NewzNabSearchResult>> Search(NewzNabQuery query)
        {
            var results = new List<NewzNabSearchResult>();
            var queryUri = new UriBuilder(ApiUrl);
            var result = new StringBuilder();
            result.Append($"t=search&apikey={ApiKey}&q={query.Query}&cat={string.Join(",", query.Categories)}");
            if (query.Offset != null) { result.Append("&offset=" + query.Offset); }
            if (query.Groups != null && query.Groups.Any())
            {
                result.Append("&group=");
                result.Append(string.Join(",", query.Groups));
            }
            queryUri.Query = result.ToString();
            var xmlResult = new XmlDocument();
            xmlResult.Load(queryUri.Uri.ToString());
            foreach (XmlNode item in xmlResult.SelectNodes("/rss/channel/item"))
            {
                results.Add(new NewzNabSearchResult(item));
            }
            return results;
        }

        public async Task<bool> Test()
        {
            try
            {
                var queryUri = new UriBuilder(ApiUrl) {Query = $"t=caps&o=xml&apikey={ApiKey}"};
                var xmlResponse = new XmlDocument();
                xmlResponse.Load(queryUri.Uri.ToString());
                var response = new NewzNabCapabilities(xmlResponse);
                Categories = response.Categories.Select(c => c.Key).Where(c => c >= 3000 && c < 4000 && c != 3020).ToList();
                return Categories.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<SearchResult>> Search(string query)
        {
            if (Categories.Count == 0)
            {
                await Test();
            }
            var nnq = new NewzNabQuery(query, Categories);
            return await Search(nnq);
        }

        public void Dispose()
        {
        }
    }
}
