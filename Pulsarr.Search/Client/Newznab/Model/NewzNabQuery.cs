using System.Collections.Generic;

namespace Pulsarr.Search.Client.Newznab.Model
{
    public class NewzNabQuery
    {
        public string Query { get; }
        public IEnumerable<string> Groups { get; }
        public IEnumerable<int> Categories { get; }
        public int? Offset { get; }

        public NewzNabQuery(string query, IEnumerable<int> categories, IEnumerable<string> groups = null, int? offset = null)
        {
            Offset = offset;
            Query = query;
            Groups = groups;
            Categories = categories;
        }
    }
}
