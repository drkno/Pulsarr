using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Pulsarr.Search.Client.Newznab.Model
{
    public struct NewzNabGenre
    {
        public NewzNabGenre(IDictionary<int, string> categories, XmlNode node)
        {
            Id = int.Parse(node.Attributes["id"].Value);
            CategoryId = int.Parse(node.Attributes["categoryid"].Value);
            Name = HttpUtility.HtmlDecode(categories[CategoryId] + "\\" + node.Attributes["name"].Value);
        }

        public int Id { get; }
        public int CategoryId { get; }
        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
