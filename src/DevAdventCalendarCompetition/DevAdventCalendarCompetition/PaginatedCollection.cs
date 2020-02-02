using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevAdventCalendarCompetition
{
    public class PaginatedCollection<T> : List<T>
    {
        public PaginatedCollection(List<T> source, int pageIndex, int pageSize)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var count = source.Count;
            var items = source.Skip(
                    (pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();

            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.PageSize = pageSize;

            this.AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public bool HasPreviousPage => this.PageIndex > 1;

        public bool HasNextPage => this.PageIndex < this.TotalPages;
    }
}
