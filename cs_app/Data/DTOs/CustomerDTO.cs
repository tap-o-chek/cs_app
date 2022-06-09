namespace cs_app.Data.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecName { get; set; }
        public int Age { get; set; }
        public int Doc { get; set; }
    
        public IEnumerable<long> Hotels { get; set; }
        public IEnumerable<long> Tickets { get; set; }
    }
}