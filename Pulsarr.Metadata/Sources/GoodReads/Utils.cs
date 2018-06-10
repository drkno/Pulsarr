using System.Text.RegularExpressions;

namespace Pulsarr.Metadata.Sources.GoodReads
{
    public static class Utils
    {
        private static readonly Regex _partRegex = new Regex(@"^(.*) \(([^\)]+) #([0-9]+(\.[0-9]+)?)\)$");

        public static (string title, string series, float? part) ExtractTitleData(string title)
        {
            string series;
            float? part;
            var match = _partRegex.Match(title);
            if (match.Success)
            {
                title = match.Groups[1].Value;
                series = match.Groups[2].Value.TrimEnd(',');
                part = float.Parse(match.Groups[3].Value);
            }
            else
            {
                series = null;
                part = null;
            }
            return (title, series, part);
        }
    }
}
