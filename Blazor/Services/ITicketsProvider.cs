using Blazor.Data.Models;

namespace Blazor.Services
{
    public interface ITicketsProvider //интерфейс, описывающий получение данных по билетам
    {
        Task<List<Ticket>> GetAll();
        Task<Ticket> GetOne(int id);
        Task<bool> Add(Ticket avia);
        Task<Ticket> Edit(Ticket avia);
        Task<bool> Remove(int id);
    }
}
