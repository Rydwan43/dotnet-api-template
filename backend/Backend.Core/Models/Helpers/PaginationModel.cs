using System.Linq;

namespace Backend.Core.Models.Helpers
{
    public class PaginationModel<TModel>
    {
        public PaginationModel(int page, int pageSize, IQueryable<TModel> items)
        {
            LastPage = items.Count() / pageSize;
            TotalItems = items.Count();
            Page = page < 0 ? 0 : page;
            Page = Page > LastPage ? LastPage : Page;
            PageSize = pageSize <= 0 ? 1 : pageSize;
            Items = items;
            Items = Items.Skip(Page * PageSize).Take(PageSize);
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int LastPage { get; set; }
        public int TotalItems { get; set; }
        public IQueryable<TModel> Items { get; set; }
    }
}