using MiniAPI.Data;
using MiniAPI.DTOs;
using MiniAPI.Models;

namespace Services
{
    public interface IToDoService
    {
        Task<ToDoResultDto> CreateAsync(ToDoDto dto);
    }

    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;
        private readonly IPublishingService _publishingService;

        public ToDoService(IToDoRepository repository, IPublishingService publishingService)
        {
            _repository = repository;
            _publishingService = publishingService;
        }

        public async Task<ToDoResultDto> CreateAsync(ToDoDto dto)
        {
            var result = new ToDoResultDto
            {
                Id = 0,
                Name = dto.Name
            };

            try
            {
                var todo =new ToDo
                {
                    Name = dto.Name
                };

                var createdResult = await _repository.CreateAsync(todo);
                if (createdResult > 0)
                {
                    result.Id = todo.Id;

                    _ = _publishingService.ProceedAsync<ToDoResultDto>(new PublishedDto<ToDoResultDto>
                    {
                        Status = "Created",
                        MessageObject = result
                    });
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}