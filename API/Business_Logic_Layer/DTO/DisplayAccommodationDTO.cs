using API.DTO;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTO
{
    public class DisplayAccommodationDTO
    {
        public int Id { get; set; }
        public int BenefactorAppUserID { get; set; }
        public int RefugeeID { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfBeds { get; set; }
        public string? City { get; set; }
        public string? PostCode { get; set; }
        public long Home_Number { get; set; }
        public long Flat_Number { get; set; }
        public DateTime? StartAccommodation { get; set; } = DateTime.Now;
        public DateTime? EndAccommodation { get; set; }
    }
}
