using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;


        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            DataContext context,
            RoleManager<AppRole> roleManager,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var roles = new List<AppRole>
            {
                new AppRole{Name = "Refugee"},
                new AppRole{Name = "Benefactor"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            if (await UserExists(registerDTO.UserName)) return BadRequest("Email is taken");

            var user = _mapper.Map<AppUser>(registerDTO);

            user.UserName = registerDTO.UserName.ToLower();

            user.Email = user.UserName;

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, registerDTO.Status);

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //var uriBuilder = new UriBuilder() { Port = -1 };
            //var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            //query["userId"] = user.Id.ToString();
            //query["code"] = code;
            //uriBuilder.Query = query.ToString();

            //await _emailSender.SendEmailAsync
            //(
            //    user.Email,
            //    "Email confirmation",
            //    $"Confirm the registration by clicking on the <a href='{uriBuilder}'>link</a>."
            //);

            //var callbackurl = this.Url.ActionLink("ConfirmEmail", "Account", new { code, email = user.Email }, Request.Scheme);

            //await _emailSender.SendEmailAsync(user.Email, "Confirm yout account - Identity Manager",
            //    "Please confirm your account by clicking here <a href=\"" + callbackurl + "\">link</a>");

            //await _signInManager.SignInAsync(user, isPersistent: false);

            //return new UserDTO
            //{
            //    UserName = user.UserName,
            //    Token = await _tokenService.CreateTokenAsync(user),
            //};
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDTO.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid email adress");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user)
            };
        }

        //[HttpPost("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword()

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
