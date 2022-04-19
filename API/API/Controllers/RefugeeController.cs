using API.Data;
using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefugeeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;


        public RefugeeController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet("GetEmptyAccommodation")]
        public async Task<ActionResult<AccommodationDTO>> GetEmptyAccommodation([FromQuery] Paging paging,[FromQuery] FilteringProperties filter)
        {
            return Ok(await _unitOfWork.RefugeeRepository.GetEmptyAccommodations(paging,filter));
        }
    }
}
