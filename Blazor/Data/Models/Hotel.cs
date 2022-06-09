using Blazor.Data.Models;

namespace Blazor.Data.Models
{
    public class Hotel
    {
        public long Id { get; set; }
        public string HotelName { get; set; }
        public int Room { get; set; }
        public string CarName { get; set; }
        public string GuideName { get; set; }
    
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<Customer> Customers { get; set; }

    }
}