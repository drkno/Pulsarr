using System;
using System.Collections.Generic;
using Pulsarr.Preferences.DataModel.Library;

namespace Pulsarr.Library.Model
{
    public class LibraryItemsResponse
    {
        public IList<LibraryItem> Items { get; }
        public int TotalItems { get; }
        public int? RangeStart { get; }
        public int? RangeEnd { get; }
        public string SortingBy { get; }
        public SortOrder SortOrder { get; }

        public LibraryItemsResponse(IList<LibraryItem> items,
            string sortBy = null,
            SortOrder sortOrder = SortOrder.Ascending,
            ulong maxResults = 0,
            ulong startAt = 0)
        {
            var result = new List<LibraryItem>(items);
            TotalItems = result.Count;
            if (sortBy != null)
            {
                SortingBy = sortBy;
                result.Sort((itemA, itemB) => LibraryItem.Compare(itemA, itemB, sortBy));
            }

            SortOrder = sortOrder;
            if (sortOrder == SortOrder.Decending)
            {
                result.Reverse();
            }

            if (maxResults != 0)
            {
                var startIndex = Math.Min((int) startAt, Math.Max(result.Count - 1, 0));
                var endIndex = Math.Min(startIndex + (int) maxResults, Math.Max(result.Count - 1, 0));
                result = result.GetRange(startIndex, endIndex - startIndex);
                RangeStart = startIndex;
                RangeEnd = endIndex;
            }

            Items = result;
        }
    }
}
