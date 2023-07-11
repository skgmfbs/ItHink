namespace SubscribingService.DTOs
{
    public class ToDoStateDto
    {
        public int ToDoId { get; set; }
        public ToDoStatus Status { get; set; }
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