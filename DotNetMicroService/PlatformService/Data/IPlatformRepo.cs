using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        void Create(Platform platform);
        Platform Get(int id);
        IEnumerable<Platform> GetAll();
        bool SaveChanges();
    }
}