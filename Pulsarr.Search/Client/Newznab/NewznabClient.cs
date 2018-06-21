using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Pulsarr.Search.Client.Newznab.Model;

namespace Pulsarr.Search.Client.Newznab
{
    public class NewzNabClient
    {
        public string ApiKey { get; }
        public Uri ApiUrl { get; }

        public NewzNabClient(string url, string apiKey, string apiPath = "/api")
        {
            ApiKey = apiKey;

            var builder = new UriBuilder(url)
            {
                Scheme = url.StartsWith("https")
                    ? Uri.UriSchemeHttps
                    : Uri.UriSchemeHttp
            };
            if (!builder.Path.EndsWith(apiPath))
            {
                builder.Path = builder.Path.TrimEnd('/') + '/' + apiPath.TrimStart('/');
            }
            builder.Query = "";
            ApiUrl = builder.Uri;
        }

        public NewzNabCapabilities GetCapabilities()
        {
            var queryUri = new UriBuilder(ApiUrl) {Query = $"t=caps&o=xml&apikey={ApiKey}"};
            var xmlResponse = new XmlDocument();
            xmlResponse.Load(queryUri.Uri.ToString());
            return new NewzNabCapabilities(xmlResponse);
        }
        
        public List<NewzNabSearchResult> Search(NewzNabQuery query)
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
    }
}
