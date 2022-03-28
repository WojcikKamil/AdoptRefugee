namespace API.DTO
{
    public class PersonDTO
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public DateTime Birth { get; set; }
        public long PESEL { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public long Home_Number { get; set; }
        public long Flat_Number { get; set; }
    }
}
