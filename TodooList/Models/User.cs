using System.ComponentModel.DataAnnotations.Schema;

namespace TodooList.Models
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public virtual List<ToDo> TodoList { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string? Image { get; set; }
        public User()
        {
            TodoList= new List<ToDo>();
        }
    }
}
