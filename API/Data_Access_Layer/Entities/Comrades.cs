namespace API.Entities
{
    public class Comrades
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public DateTime Birth { get; set; }
        public Refugee? Refugee { get; set; }
    }
}
