using Business_Logic_Layer.DTO;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IRequestRepository
    {
        void AddRequest(Request requests);
        Task<IEnumerable<DisplayRequestDTO>> GetRequests(int id);
        Task<Request> GetRequest(int id);
        Task<List<Request>> CheckRequest (int SenderID, int RecipientID);
        void DeleteRequest(Request requests);
    }
}
