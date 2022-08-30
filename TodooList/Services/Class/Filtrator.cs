using TodooList.Models;
using TodooList.Services.Interface;

namespace TodooList.Services.Class
{
    public class Filtrator : IFiltrator<User>
    {
        public IQueryable<User> Filter(IQueryable<User> source, string searchstring)
        {
            if (searchstring != null)
            {
                source = source.Where(u => u.Name!.Contains(searchstring));
            }
            return source;
        }
    }
}
