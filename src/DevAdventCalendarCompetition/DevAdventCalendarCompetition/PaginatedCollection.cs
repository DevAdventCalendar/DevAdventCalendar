using System;
using System.Collections.Generic;

namespace DevAdventCalendarCompetition
{
    public class PaginatedCollection<T> : List<T>
    {
        public PaginatedCollection(List<T> source, int pageIndex, int pageSize, int totalPages)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var count = source.Count;
            var items = source;

            this.PageIndex = pageIndex;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;

            this.AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool HasPreviousPage => this.PageIndex > 1;

        public bool HasNextPage => this.PageIndex < this.TotalPages;

        private int TotalPages { get; set; }
    }
}