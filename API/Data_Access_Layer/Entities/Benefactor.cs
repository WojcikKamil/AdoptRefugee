using Data_Access_Layer.Entities.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Benefactor : Person
    {
        public long PESEL { get; set; }
    }
}
