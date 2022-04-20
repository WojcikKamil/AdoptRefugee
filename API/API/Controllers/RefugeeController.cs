using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefugeeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RefugeeController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetEmptyAccommodation")]
        public async Task<ActionResult<AccommodationDTO>> GetEmptyAccommodation([FromQuery] Paging paging,[FromQuery] FilteringProperties filter)
        {
            return Ok(await _unitOfWork.RefugeeRepository.GetEmptyAccommodations(paging,filter));
        }

        [Authorize(Policy = "RequireRefugeeRole")]
        [HttpPost("add-comrades")]
        public async Task<ActionResult> PostComrades([FromBody] ComradesDTO comradesDTO)
        {

            var email = User.GetUserName();

            if (email == null) return NotFound();

            var user = await _unitOfWork.PersonRepository.GetByEmail(email);

            if (user == null) return NotFound();

            var comrades = new Comrades
            {
                PersonID = user.Id,
                Name = comradesDTO.Name,
                LastName = comradesDTO.LastName,
                Gender = comradesDTO.Gender,
                Nationality = comradesDTO.Nationality,
                Birth = comradesDTO.Birth
            };

            _unitOfWork.PersonRepository.AddComrades(comrades);

            if (await _unitOfWork.Done())
                return Ok(_mapper.Map<ComradesDTO>(comrades));

            return BadRequest("Failed");
        }

        [HttpGet("GetAccommodationByBenefactorID")]
        public async Task<ActionResult<AccommodationDTO>> GetAccomodationsByBeneID([FromQuery] int BeneId)
        {
            return Ok(await _unitOfWork.RefugeeRepository.GetAccommodationByBenefactorId(BeneId));
        }
    }
}
