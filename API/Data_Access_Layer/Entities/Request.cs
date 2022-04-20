using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int SenderBenefactorId { get; set; }
        public int RecipientRefugeeId { get; set; }
        public DateTime? SendingTime { get; set; } = DateTime.Now;
        public string ?RequestStatus { get; set; }
        public AppUser ?AppUser { get; set; }
    }
}
