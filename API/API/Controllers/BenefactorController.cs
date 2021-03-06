using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Business_Logic_Layer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenefactorController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;


        public BenefactorController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPhotoService photoService
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }

        [Authorize(Policy = "RequireBenefactorRole")]
        [HttpPost("add-accommodation")]
        public async Task<ActionResult<AccommodationDTO>> PostAccommodation(AccommodationDTO accommodationDTO)
        {
            var email = User.GetUserName();

            if (email == null) return NotFound();

            var user = await _unitOfWork.PersonRepository.GetByEmail(email);

            if (user == null) return NotFound();

            var accommodation = new Accommodation
            {
                BenefactorAppUserID = user.Id,
                NumOfRooms = accommodationDTO.NumOfRooms,
                NumOfBeds = accommodationDTO.NumOfBeds,
                City = accommodationDTO.City,
                PostCode = accommodationDTO.PostCode,
                Home_Number = accommodationDTO.Home_Number,
                Flat_Number = accommodationDTO.Flat_Number
            };

            _unitOfWork.AccommodationRepository.AddAccommodation(accommodation);

            if (await _unitOfWork.Done())
                return Ok(_mapper.Map<AccommodationDTO>(accommodation));

            return BadRequest("Failed");
        }

        //[HttpGet("GetAccommodation")]
        //public async Task<ActionResult<DisplayAccommodationDTO>> GetAccommodation()
        //{
        //    var email = User.GetUserName();

        //    if (email == null) return NotFound();

        //    var user = await _unitOfWork.PersonRepository.GetByEmail(email);

        //    return Ok(await _unitOfWork.AccommodationRepository.GetAccommodation(user.Id));
        //}

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            var email = User.GetUserName();

            if (email == null) return NotFound();

            var user = await _unitOfWork.PersonRepository.GetByEmail(email);

            if (user == null) return NotFound();

            var dupa = await _unitOfWork.AccommodationRepository.GetAccommodation(user.Id);

            var result = await _photoService.AddPhoto(file);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            dupa.Photos?.Add(photo);

            if (await _unitOfWork.Done())
                return _mapper.Map<PhotoDTO>(photo);

            return BadRequest("Problem with adding photo");
        }

        [HttpGet("GetHomelessRefugees")]
        public async Task<ActionResult<PersonDTO>> GetEmptyAccommodation()
        {
            return Ok(await _unitOfWork.BenefactorRepository.GetHomelessRefuges());
        }

        [HttpGet("GetAllFamily")]
        public async Task<ActionResult<PersonDTO>> GetFamily(int id)
        {
            return Ok(await _unitOfWork.BenefactorRepository.GetFamily(id));
        }
    }
}
