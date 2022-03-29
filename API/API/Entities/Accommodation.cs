namespace API.Entities
{
    public class Accommodation
    {
        public int Id { get; set; }
        public int BenefactorID { get; set; }
        public int RefugeeID { get; set; }
        public ICollection<Photo> ?Photos { get; set; }
    }
}
