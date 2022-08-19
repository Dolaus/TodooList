namespace TodooList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<ToDo> TodoList { get; set; }
    }
}
