using Microsoft.EntityFrameworkCore;
using TodooList.Models;
using TodooList.Services.Interface;

namespace TodooList.Services.Class
{
    public class Paginator:IPaginator<User>
    {
        public async Task<IndexViewModel> Pagination(int pageSize,IQueryable<User> source,int page=1)
        {
            
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };
            return viewModel;
        }

    }
}
