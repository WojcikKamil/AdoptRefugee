using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTO
{
    public class DisplayRequestDTO
    {
        public int Id { get; set; }
        public int SenderBenefactorId { get; set; }
        public int RecipientRefugeeId { get; set; }
        public DateTime? SendingTime { get; set; } 
        public string? RequestStatus { get; set; }
    }
}
