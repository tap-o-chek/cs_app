namespace cs_app.Data.DTOs
{
    public class HotelDTO
    {
        public long Id { get; set; }
        public string Hotel { get; set; }
        public int Room { get; set; }
        public string CarName { get; set; }
        public string GuideName { get; set; }
    
        public IEnumerable<long> Tickets { get; set; }
        public IEnumerable<long> Customers { get; set; }

    }
}