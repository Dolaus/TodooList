namespace TodooList.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; } = false;
        public int? IdToDo { get; set; }
        public virtual User User { get; set; }
    }
}
