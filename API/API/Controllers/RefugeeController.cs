using API.Data;
using API.DTO;
using API.Entities;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;


        public RefugeeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            DataContext context,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _signInManager = signInManager;
            _context = context;
            _unitOfWork = unitOfWork;

        }

        [HttpGet("GetEmptyAccommodation")]
        public async Task<ActionResult<AccommodationDTO>> GetEmptyAccommodation()
        {
            return Ok(await _unitOfWork.RefugeeRepository.GetEmptyAccommodations());
        }
    }
}
