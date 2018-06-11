using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Pulsarr.Search.Client.Newznab.Model
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, TypeName = "caps")]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class Capabilities
    {
        [XmlAttribute("server")]
        public ServerCapabilities Server { get; set; }
        [XmlAttribute("limits")]
        public ServerLimits Limits { get; set; }
        [XmlAttribute("retention")]
        public ServerRetention Retention { get; set; }
        [XmlAttribute("registration")]
        public Registration Registration { get; set; }
        [XmlAttribute("searching")]
        public Searching Searching { get; set; }
        [XmlArrayItem("category", IsNullable = false)]
        public Category[] Categories { get; set; }
    }
    
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ServerCapabilities
    {
        [XmlAttribute("version")]
        public decimal Version { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("strapline")]
        public string Strapline { get; set; }
        [XmlAttribute("email")]
        public string Email { get; set; }
        [XmlAttribute("url")]
        public string Url { get; set; }
        [XmlAttribute("image")]
        public string Image { get; set; }
    }
    
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ServerLimits
    {
        [XmlAttribute("max")]
        public int Max { get; set; }
        [XmlAttribute("default")]
        public int @Default { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ServerRetention
    {
        [XmlAttribute("days")]
        public ushort Days { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Registration
    {
        [XmlAttribute("available")]
        public string Available { get; set; }
        [XmlAttribute("open")]
        public string Open { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Searching
    {
        [XmlElement("search")]
        public SearchTypeCapabilities Search { get; set; }
        [XmlElement("tv-search")]
        public SearchTypeCapabilities TvSearch { get; set; }
        [XmlElement("movie-search")]
        public SearchTypeCapabilities MovieSearch { get; set; }
    }
    
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SearchTypeCapabilities
    {
        [XmlAttribute("available")]
        public string Available { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Category
    {
        [XmlElement("subcat")]
        public Subcategory[] Subcategories { get; set; }
        [XmlAttribute("id")]
        public uint Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("description")]
        public string Description { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Subcategory
    {
        [XmlAttribute("id")]
        public uint Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("description")]
        public string Description { get; set; }
    }
}
