namespace TodooList.Services.Interface
{
    public interface IFiltrator<T>
    {
        public IQueryable<T> Filter(IQueryable<T> source, string searchstring);
    }
}
