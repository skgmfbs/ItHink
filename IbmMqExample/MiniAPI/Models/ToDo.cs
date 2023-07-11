using System.ComponentModel.DataAnnotations;

namespace MiniAPI.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ToDoState> States { get; set; } = new List<ToDoState>();
    }
}