using System;
using System.Web;
using System.Xml;

namespace Pulsarr.Search.Client.Newznab.Model
{
    public struct UsenetGroup
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime LastUpdate { get; }

        public UsenetGroup(XmlNode group)
        {
            Id = int.Parse(group.Attributes["id"].Value);
            Name = HttpUtility.HtmlDecode(group.Attributes["name"].Value);
            Description = group.Attributes["description"].Value;
            DateTime.TryParse(group.Attributes["lastupdate"].Value, out var lastUpdate);
            LastUpdate = lastUpdate;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}