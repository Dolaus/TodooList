using Microsoft.Build.Framework;

namespace TodooList.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsComplete { get; set; } = false;
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
