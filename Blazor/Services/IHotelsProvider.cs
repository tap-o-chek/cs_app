using Blazor.Data.Models;

namespace Blazor.Services
{
    public interface IHotelProvider //интерфейс, описывающий получение данных по отелям
    {
        Task<List<Hotel>> GetAll();
        Task<Hotel> GetOne(int id);
        Task<bool> Add(Hotel avia);
        Task<Hotel> Edit(Hotel avia);
        Task<bool> Remove(int id);

    }
}