using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities.models
{
    public class Person
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public DateTime Birth { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public long Home_Number { get; set; }
        public long Flat_Number { get; set; }
        public string? Status { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
