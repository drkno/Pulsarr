using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using Pulsarr.Search.Client.Newznab.Model;
using Pulsarr.Search.Utils;

namespace Pulsarr.Search.Client.Newznab
{
    public class NewzNabCapabilities
    {
        public int MaxResults { get; }
        public int DefaultResults { get; }
        public int Retention { get; }

        public bool SearchAvail { get; }
        public bool TVSearchAvail { get; }
        public bool MovieSearchAvail { get; }
        public bool AudioSearchAvail { get; }
        public IDictionary<int, string> Categories { get; }
        public IList<UsenetGroup> Groups { get; }
        public IList<NewzNabGenre> Genres { get; }

        public NewzNabCapabilities(XmlNode xmlResponse)
        {
            MaxResults = int.Parse(xmlResponse.SelectSingleNode("/caps/limits")?.Attributes["max"]?.Value ?? "-1");
            DefaultResults = int.Parse(xmlResponse.SelectSingleNode("/caps/limits")?.Attributes["default"]?.Value ?? "-1");
            Retention = int.Parse(xmlResponse.SelectSingleNode("/caps/retention")?.Attributes["days"]?.Value ?? "-1");

            SearchAvail = xmlResponse.SelectSingleNode("/caps/searching/search")?.Attributes["available"]?.Value?.FromYesNo() ?? false;
            TVSearchAvail = xmlResponse.SelectSingleNode("/caps/searching/tv-search")?.Attributes["available"]?.Value?.FromYesNo() ?? false;
            MovieSearchAvail = xmlResponse.SelectSingleNode("/caps/searching/movie-search")?.Attributes["available"]?.Value?.FromYesNo() ?? false;
            AudioSearchAvail = xmlResponse.SelectSingleNode("/caps/searching/audio-search")?.Attributes["available"]?.Value?.FromYesNo() ?? false;

            Categories = new Dictionary<int, string>();
            foreach (XmlNode cat in xmlResponse.SelectNodes("caps/categories/category"))
            {
                Categories.Add(int.Parse(cat.Attributes["id"].Value), HttpUtility.HtmlDecode(cat.Attributes["name"].Value));
                foreach (XmlNode subCat in cat.ChildNodes)
                {
                    Categories.Add(int.Parse(subCat.Attributes["id"].Value), HttpUtility.HtmlDecode(cat.Attributes["name"].Value + "\\" + subCat.Attributes["name"].Value));
                }
            }

            Groups = new List<UsenetGroup>();
            foreach (XmlNode group in xmlResponse.SelectNodes("caps/groups/group"))
            {
                Groups.Add(new UsenetGroup(group));
            }

            Genres = new List<NewzNabGenre>();
            foreach (XmlNode genre in xmlResponse.SelectNodes("caps/genres/genres"))
            {
                Genres.Add(new NewzNabGenre(Categories, genre));
            }
        }
    }
}
