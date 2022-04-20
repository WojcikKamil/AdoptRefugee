using API.Data;
using AutoMapper;
using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Repositories
{
    public class RequestRepository : IRequestRepository
    {

            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public RequestRepository(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public void AddRequest(Request requests)
        {
            _context?.Requests?.Add(requests);
        }

        public Task<List<Request>> CheckRequest(int SenderID, int RecipientID)
        {
            return _context.Requests!
                .Where(x => x.SenderBenefactorId == SenderID && x.RecipientRefugeeId == RecipientID)
                .ToListAsync();
        }

        public void DeleteRequest(Request requests)
        {
            _context.Requests?.Remove(requests);
        }

        public async Task<Request> GetRequest(int id)
        {
            return await _context.Requests
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<DisplayRequestDTO>> GetRequests(int id)
        {
            var request = await _context.Requests!
                .Where(x => x.RecipientRefugeeId == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DisplayRequestDTO>>(request);
        }
    }
}
