namespace TodooList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public virtual List<ToDo> TodoList { get; set; }

        public User()
        {
            TodoList= new List<ToDo>();
        }
    }
}
