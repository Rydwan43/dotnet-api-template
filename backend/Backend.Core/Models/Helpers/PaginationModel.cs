using System;
using System.Linq;

namespace Backend.Core.Models.Helpers
{
    public class PaginationModel<TModel>
    {
        public PaginationModel(int page, int pageSize, IQueryable<TModel> source)
        {
            TotalItems = source.Count();
            PageSize = pageSize <= 0 ? 1 : pageSize;
            LastPage = (int)Math.Ceiling((decimal)TotalItems / (decimal)pageSize);
            Page = page < 1 ? 1 : page;
            Page = Page >= LastPage ? LastPage : Page;
            Items = source;
            Items = Items.Skip((Page - 1) * PageSize).Take(PageSize);
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int LastPage { get; set; }
        public int TotalItems { get; set; }
        public IQueryable<TModel> Items { get; set; }
    }
}