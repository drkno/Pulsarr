using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml;
using Pulsarr.Search.Manager.Model;

namespace Pulsarr.Search.Client.Newznab.Model
{
    public class NewzNabSearchResult : SearchResult
    {
        private readonly IDictionary<string, string> _attributes;

        public string Title { get; }
        public string Id => Guid;
        public string Guid { get; }
        public string Link { get; }
        public string CommentLink { get; }
        public DateTime PublishDate { get; }
        public string Category { get; }
        public string Description { get; }
        public string NZBUrl { get; }
        public IReadOnlyDictionary<string, string> Attributes => new ReadOnlyDictionary<string, string>(_attributes);

        public NewzNabSearchResult(XmlNode node)
        {
            _attributes = new Dictionary<string, string>();
            Title = node["title"].InnerText;
            Guid = node["guid"].InnerText;
            Link = node["link"].InnerText;
            CommentLink = node["comments"].InnerText;
            PublishDate = DateTime.Parse(node["pubDate"].InnerText);
            Category = node["category"].InnerText;
            Description = Regex.Replace(node["description"].InnerText.Replace(@"<br />",Environment.NewLine), "<.*?>", string.Empty).Trim();
            NZBUrl = node["enclosure"].GetAttribute("url");
            foreach (XmlNode attr in node.ChildNodes)
            {
                if (attr.Name == "newznab:attr")
                {
                    _attributes[attr.Attributes["name"].Value] = attr.Attributes["value"].Value;
                }
            }
        }
    }
}
