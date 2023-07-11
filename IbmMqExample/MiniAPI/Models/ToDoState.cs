using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniAPI.Models
{
    public class ToDoState
    {
        [Key]
        public int Id { get; set; }
        public ToDoStatus Status { get; set; }
        public int ToDoId { get; set; }

        [JsonIgnore]
        public ToDo? ToDo { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public enum ToDoStatus
    {
        Todo,
        InProgress,
        Waiting,
        Cancel,
        Done
    }
}