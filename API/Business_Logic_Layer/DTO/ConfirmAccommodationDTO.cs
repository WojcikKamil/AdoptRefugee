using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTO
{
    public class ConfirmAccommodationDTO
    {
        public int BenefactorID { get; set; }
        public int RefugeeID { get; set; }
        public DateTime? StartAccommodation { get; set; } = DateTime.Now;
    }
}
