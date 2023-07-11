using MiniAPI.Models;

namespace MiniAPI.DTOs
{
    public class ToDoStateDto
    {
        public int ToDoId { get; set; }
        public ToDoStatus Status { get; set; }
    }
    public class ToDoStateResultDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int ToDoId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}