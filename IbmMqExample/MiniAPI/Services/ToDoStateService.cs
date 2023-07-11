using MiniAPI.Data;
using MiniAPI.DTOs;
using MiniAPI.Models;

namespace Services
{
    public interface IToDoStateService
    {
        Task<ToDoStateResultDto> AddAsync(ToDoStateDto dto);
    }

    public class ToDoStateService : IToDoStateService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IToDoStateRepository _toDoStateRepository;

        public ToDoStateService(IToDoRepository toDoRepository, IToDoStateRepository toDoStateRepository)
        {
            _toDoRepository = toDoRepository;
            _toDoStateRepository = toDoStateRepository;
        }

        public async Task<ToDoStateResultDto> AddAsync(ToDoStateDto dto)
        {
            var result = new ToDoStateResultDto
            {
                Id = 0,
                ToDoId = dto.ToDoId,
                Status = dto.Status
            };

            try
            {
                var toDo = await _toDoRepository.GetByIdAsync(dto.ToDoId);
                if (toDo != null)
                {
                    var toDoState = new ToDoState
                    {
                        ToDoId = dto.ToDoId,
                        Status = dto.Status,
                        CreatedDate = DateTime.Now
                    };
                    var createdResult = await _toDoStateRepository.CreateAsync(toDoState);
                    if (createdResult > 0)
                    {
                        result.Id = toDoState.Id;
                    }
                }
                else
                {
                    result.Message = $"ToDo '{dto.ToDoId}' not found";
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