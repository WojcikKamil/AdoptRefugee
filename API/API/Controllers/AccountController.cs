using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public AccountController(
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        //[Authorize(Policy = "RequireBenefactorRole")]
        [Authorize]
        [HttpPost("add-data")]
        public async Task<ActionResult> PostData([FromBody] PersonDTO personDTO)
        {
            var email = User.GetUserName();

            var user = await _unitOfWork.PersonRepository.GetByEmail(email!);

            if (user == null) return NotFound();

            if (user.Status == "Refugee")
            {
                var refugee = new Refugee
                {
                    AppUserId = user.Id,
                    Name = personDTO.Name,
                    LastName = personDTO.LastName,
                    Gender = personDTO.Gender,
                    Nationality = personDTO.Nationality,
                    Birth = personDTO.Birth,
                    City = personDTO.City,
                    Street = personDTO.Street,
                    Status = user.Status,
                    Home_Number = personDTO.Home_Number,
                    Flat_Number = personDTO.Flat_Number,
                };
                _unitOfWork.PersonRepository.AddRefugeeData(refugee);

                if (await _unitOfWork.Done())
                    return Ok(_mapper.Map<PersonDTO>(refugee));

                return BadRequest("Failed to add Refugee data");
            }
            else
            {
                var benefactor = new Benefactor
                {
                    AppUserId = user.Id,
                    Name = personDTO.Name,
                    LastName = personDTO.LastName,
                    Gender = personDTO.Gender,
                    Nationality = personDTO.Nationality,
                    Birth = personDTO.Birth,
                    City = personDTO.City,
                    Street = personDTO.Street,
                    Status = user.Status,
                    Home_Number = personDTO.Home_Number,
                    Flat_Number = personDTO.Flat_Number,
                };

                _unitOfWork.PersonRepository.AddBenefactorData(benefactor);

                if (await _unitOfWork.Done())
                    return Ok(_mapper.Map<PersonDTO>(benefactor));

                return BadRequest("Failed to add Benefactor data");
            }

            return BadRequest("Failed");
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
    }
}
