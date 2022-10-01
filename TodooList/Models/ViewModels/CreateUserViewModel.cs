namespace TodooList.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Year { get; set; }
        public string? Image { get; set; }
        public Role? Role { get; set; }
    }
}
