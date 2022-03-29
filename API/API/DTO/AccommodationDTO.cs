namespace API.DTO
{
    public class AccommodationDTO
    {
        public int BenefactorID { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfBeds { get; set; }
        public string? City { get; set; }
        public string? PostCode { get; set; }
        public long Home_Number { get; set; }
        public long Flat_Number { get; set; }
        public string ?PhotoUrl { get; set; }
        public ICollection<PhotoDTO> ?Photos { get; set; }
    }
}
