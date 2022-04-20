using API.DTO;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Business_Logic_Layer.DTO;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public RequestsController(
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "RequireBenefactorRole")]
        [HttpPost("add-request")]
        public async Task<ActionResult<SendRequestDTO>> PostRequest(SendRequestDTO requestDTO)
        {
            var email = User.GetUserName();

            if (email == null) return NotFound();

            var benefactor = await _unitOfWork.PersonRepository.GetByEmail(email);

            if (benefactor == null) return NotFound();

            var refugee = await _unitOfWork.PersonRepository.GetById(requestDTO.RecipientRefugeeId);

            if (refugee == null) return BadRequest("Recipient doesnt exist");

            if (benefactor == refugee ) return BadRequest("Are u fucking kidding me? HOW??????????");

            var check = await _unitOfWork.RequestRepository.CheckRequest(benefactor.Id,refugee.Id);

            if (check.Count > 0) return BadRequest("You have already invite them");

            var request = new Request
            {
                SenderBenefactorId = benefactor.Id,
                RecipientRefugeeId = requestDTO.RecipientRefugeeId,
                SendingTime = DateTime.Now,
                RequestStatus = "Waiting",
            };

            _unitOfWork.RequestRepository.AddRequest(request);

            if (await _unitOfWork.Done())
                return Ok(_mapper.Map<SendRequestDTO>(request));

            return BadRequest("Failed to request");
        }

        [Authorize(Policy = "RequireRefugeeRole")]
        [HttpGet("get-requests")]
        public async Task<ActionResult<IEnumerable<DisplayRequestDTO>>> GetRequests()
        {
            var email = User.GetUserName();

            if (email == null) return NotFound();

            var user = await _unitOfWork.PersonRepository.GetByEmail(email);

            if(user == null) return NotFound();

            return Ok(await _unitOfWork.RequestRepository.GetRequests(user.Id));
        }

        [Authorize(Policy = "RequireRefugeeRole")]
        [HttpPut("accept-request")]
        public async Task<ActionResult<ConfirmAccommodationDTO>> 
            AcceptAccommodationRequest(ConfirmAccommodationDTO confirmAccommodationDTO)
        {
            var email = User.GetUserName();

            if (email == null) return NotFound();

            var user = await _unitOfWork.PersonRepository.GetByEmail(email);

            if (user == null) return NotFound();

            var accommodation = await _unitOfWork.AccommodationRepository.GetOneAccommodation(confirmAccommodationDTO.BenefactorID);

            var check = await _unitOfWork.RequestRepository.CheckRequest(accommodation.BenefactorAppUserID, user.Id);

            if (check.Count == 0) return BadRequest("Poker Face");

            confirmAccommodationDTO.RefugeeID = user.Id;

            var ala = _mapper.Map(confirmAccommodationDTO, accommodation);

            _unitOfWork.AccommodationRepository.update(ala);

            if (await _unitOfWork.Done())

            {
                var req = await _unitOfWork.RequestRepository.GetRequest(accommodation.Id);

                _unitOfWork.RequestRepository.DeleteRequest(req);

                return NoContent();
            }

            return BadRequest("Failed to confirm accommodation");
        }
    }
}
