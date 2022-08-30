using TodooList.Models;

namespace TodooList.Services.Interface
{
    public  interface IPaginator<T>
    {
        public Task<IndexViewModel> Pagination(int pageSize, IQueryable<T> source, int page = 1);
    }
}
