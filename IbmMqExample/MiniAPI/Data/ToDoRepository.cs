using Microsoft.EntityFrameworkCore;
using MiniAPI.Models;

namespace MiniAPI.Data
{
    public interface IToDoRepository
    {
        Task<IReadOnlyList<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(int id);
        Task<int> CreateAsync(ToDo toDo);
    }

    public class ToDoRepository : IToDoRepository
    {
        private AppDbContext _dbContext;

        public ToDoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<ToDo>> GetAllAsync()
        {
            return await _dbContext.ToDos.ToListAsync();
        }

        public async Task<ToDo?> GetByIdAsync(int id)
        {
            return await _dbContext.ToDos.FindAsync(id);
        }

        public async Task<int> CreateAsync(ToDo toDo)
        {
            await _dbContext.ToDos.AddAsync(toDo);
            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult;
        }
    }
}