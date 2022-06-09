using cs_app.Data.DTOs;
using cs_app.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace cs_app.Data.Services
{
    public class TicketService
    {
        private EducationContext _context;

        public TicketService(EducationContext context)
        {
            _context = context;
        }


        //метод создания отеля
        public async Task<Ticket?> AddTicket(TicketDTO ticket)
        {
            Ticket nticket = new Ticket
            {
                Id = ticket.Id,
                Dest = ticket.Dest,
                Place = ticket.Place,
                NumPass = ticket.NumPass,
                Flight = ticket.Flight,
                Country = ticket.Country,
                Price = ticket.Price,
            };
            if (ticket.Customers.Any())
            {
                nticket.Customers = _context.Customers.ToList()
                    .IntersectBy(ticket.Customers, affiliation => affiliation.Id)
                    .ToList();
            }

            var result = _context.Tickets.Add(nticket);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }

        //Метод получения отеля
        public async Task<List<Ticket>> GetTickets()
        {
            var result = await _context.Tickets.Include(a => a.Customers).Include(b => b.Hotels).ToListAsync();
            return await Task.FromResult(result);
        }

        //Метод получения отеля по ID
        public async Task<Ticket?> GetTicket(long id)
        {
            var result = _context.Tickets.Include(a => a.Customers).Include(b => b.Hotels)
                .FirstOrDefault(ticket => ticket.Id == id);

            return await Task.FromResult(result);
        }

        //Метод получения неполной информации об отеле
        public async Task<List<IncompleteTicketDTO>> GetTicketsIncomplete()
        {
            var tickets = await _context.Tickets.ToListAsync();
            List<IncompleteTicketDTO> result = new List<IncompleteTicketDTO>();
            tickets.ForEach(au => result.Add(new IncompleteTicketDTO
            {
                Id = au.Id,
                Country = au.Country,
                Price = au.Price,
            }));
            return await Task.FromResult(result);
        }

        //Метод обновления информации об отеле
        public async Task<Ticket?> UpdateTicket(long id, TicketDTO updatedTicket)
        {
            var ticket = await _context.Tickets.Include(ticket => ticket.Customers).Include(b => b.Hotels)
                .FirstOrDefaultAsync(au => au.Id == id);
            if (ticket != null)
            {
                ticket.Country = updatedTicket.Country;
                ticket.Price = updatedTicket.Price;
                ticket.Place = updatedTicket.Place;
                ticket.Dest = updatedTicket.Dest;
                ticket.NumPass = updatedTicket.NumPass;
                ticket.Flight = updatedTicket.Flight;
                if (updatedTicket.Customers.Any())
                {
                    ticket.Customers = _context.Customers.ToList()
                        .IntersectBy(updatedTicket.Customers, passenger => passenger.Id).ToList();
                }

                if (updatedTicket.Hotels.Any())
                {
                    ticket.Hotels = _context.Hotels.ToList().
                        IntersectBy(updatedTicket.Hotels, bookings => bookings.Id).ToList();
                }

                _context.Tickets.Update(ticket);
                _context.Entry(ticket).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return ticket;
            }
            return null;
        }

        //метод удаления отеля
        public async Task<bool> DeleteTicket(long id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(au => au.Id == id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}