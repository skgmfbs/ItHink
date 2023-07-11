using Microsoft.EntityFrameworkCore;
using MiniAPI.Models;

namespace MiniAPI.Data
{
    public interface IToDoStateRepository
    {
        Task<IReadOnlyList<ToDoState>> GetAllAsync();
        Task<int> CreateAsync(ToDoState toDoState);
    }

    public class ToDoStateRepository : IToDoStateRepository
    {
        private AppDbContext _dbContext;

        public ToDoStateRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<ToDoState>> GetAllAsync()
        {
            return await _dbContext.ToDoStates.ToListAsync();
        }

        public async Task<int> CreateAsync(ToDoState toDoState)
        {
            await _dbContext.ToDoStates.AddAsync(toDoState);
            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult;
        }
    }
}