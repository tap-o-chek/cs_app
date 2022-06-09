using cs_app.Data.DTOs;
using cs_app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace cs_app.Data.Services
{
    public class HotelService
    {
        private EducationContext _context;

        public HotelService(EducationContext context)
        {
            _context = context;
        }


        //метод создания отеля
        public async Task<Hotel?> AddHotel(HotelDTO hotel)
        {
            Hotel nhotel = new Hotel
            {
                HotelName = hotel.Hotel,
                Room = hotel.Room,
                Id = hotel.Id,
                CarName = hotel.CarName,
                GuideName = hotel.GuideName,
            };
            if (hotel.Tickets.Any())
            {
                nhotel.Tickets = _context.Tickets.ToList().IntersectBy(hotel.Tickets, tickets => tickets.Id).ToList();
            }


            var result = _context.Hotels.Add(nhotel);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }

        //Метод получения отеля
        public async Task<List<Hotel>> GetHotels()
        {
            var result = await _context.Hotels.Include(a => a.Customers).Include(b => b.Tickets).ToListAsync();
            return await Task.FromResult(result);
        }

        //Метод получения отеля по ID
        public async Task<Hotel?> GetHotel(int id)
        {
            var result = _context.Hotels.Include(a => a.Customers).Include(b => b.Tickets)
                .FirstOrDefault(hotel => hotel.Id == id);

            return await Task.FromResult(result);
        }

        //Метод получения неполной информации об отеле
        public async Task<List<IncompleteHotelDTO>> GetHotelsIncomplete()
        {
            var hotels = await _context.Hotels.ToListAsync();
            List<IncompleteHotelDTO> result = new List<IncompleteHotelDTO>();
            hotels.ForEach(au => result.Add(new IncompleteHotelDTO
            {
                Id = au.Id,
                Hotel = au.HotelName
            }));
            return await Task.FromResult(result);
        }

        //Метод обновления информации об отеле
        public async Task<Hotel?> UpdateHotel(int id, HotelDTO updatedHotel)
        {
            var hotel = await _context.Hotels.
                Include(hotel => hotel.Customers).
                Include(b => b.Tickets)
                .FirstOrDefaultAsync(au => au.Id == id);
            if (hotel != null)
            {
                hotel.HotelName = updatedHotel.Hotel;
                hotel.Room = updatedHotel.Room;
                hotel.CarName = updatedHotel.CarName;
                hotel.GuideName = updatedHotel.GuideName;
                if (updatedHotel.Tickets.Any())
                {
                    hotel.Tickets = _context.Tickets.ToList().IntersectBy(updatedHotel.Tickets, ticket => ticket.Id).ToList();
                }

                if (updatedHotel.Customers.Any())
                {
                    hotel.Customers = _context.Customers.ToList()
                        .IntersectBy(updatedHotel.Customers, customer => customer.Id).ToList();
                }

                _context.Hotels.Update(hotel);
                _context.Entry(hotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return hotel;
            }

            return null;
        }

        //метод удаления отеля
        public async Task<bool> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(au => au.Id == id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}