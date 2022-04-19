using Data_Access_Layer.Entities.models;

namespace API.Entities
{
    public class Refugee : Person
    {
        public ICollection<Comrades>? Comrades { get; set; }
    }
}
