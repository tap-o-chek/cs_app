using Blazor.Data.Models;

namespace Blazor.Data.Models
{
    public class Ticket
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public decimal Price { get; set; }
        public string Dest { get; set; }
        public string Place { get; set; }
        public int NumPass { get; set; }
        public int Flight { get; set; }
    
        public IEnumerable<Hotel> Hotels { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}