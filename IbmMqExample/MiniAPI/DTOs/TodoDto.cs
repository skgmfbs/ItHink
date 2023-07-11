namespace MiniAPI.DTOs
{
    public struct ToDoDto
    {
        public string? Name { get; set; }
    }
    public struct ToDoResultDto
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? Name { get; set; }
    }
}