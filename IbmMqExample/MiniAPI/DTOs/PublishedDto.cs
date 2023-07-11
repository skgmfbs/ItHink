namespace MiniAPI.DTOs
{
    public struct PublishedDto<T> where T : struct
    {
        public string? Status { get; set; }
        public T MessageObject { get; set; }
    }
}