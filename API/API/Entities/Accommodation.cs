using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Accommodation
    {
        public int Id { get; set; }
        public int BenefactorID { get; set; }
        public int RefugeeID { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfBeds { get; set; }
        public string ?City { get; set; }
        public string ?PostCode { get; set; }
        public long Home_Number { get; set; }
        public long Flat_Number { get; set; }
        [JsonIgnore]
        public ICollection<Photo> ?Photos { get; set; }
        public AppUser ?AppUser { get; set; }
    }
}
