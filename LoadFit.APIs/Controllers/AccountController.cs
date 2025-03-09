using AutoMapper;
using LoadFit.APIs.DTOs;
using LoadFit.APIs.Errors;
using LoadFit.APIs.Extensions;
using LoadFit.Core;
using LoadFit.Core.Entities;
using LoadFit.Core.Entities.Identity;
using LoadFit.Core.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoadFit.APIs.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]   // POST : /api/Account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(result.Succeeded is false) return Unauthorized(new ApiResponse(401));

            //  Get the user's role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(); 

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                Role = role
            });
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email already exists" } });

            // Create User
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber,
                LicenseNumber = model.LicenseNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            // Assign Role
            if (!string.IsNullOrEmpty(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            // If the role is Driver, create a Driver entity
            if (model.Role?.ToLower() == "driver")
            {
                var brand = await _unitOfWork.Repository<VehicleBrand>().GetAsync(model.Vehicle.BrandId);
                var type = await _unitOfWork.Repository<VehicleType>().GetAsync(model.Vehicle.TypeId);

                if (brand == null || type == null)
                {
                    return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "Invalid BrandId or TypeId" } });
                }

                var driver = new Driver
                {
                    UserId = user.Id,
                    LicenseNumber = model.LicenseNumber,
                    Vehicle = _mapper.Map<Vehicle>(model.Vehicle)  // Automap VehicleDto
                };

                driver.Vehicle.Brand = brand;
                driver.Vehicle.Type = type;

                await _unitOfWork.Repository<Driver>().AddAsync(driver);
                await _unitOfWork.CompleteAsync();
            }

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                Role = model.Role
            });
        }



        //[HttpPost("register")]      // POST : /api/account/register
        //public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        //{
        //    if (CheckEmailExists(model.Email).Result.Value)
        //        return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "this Email is already Exist" } });


        //    // Create User
        //    var user = new AppUser()
        //    {
        //        DisplayName = model.DisplayName,
        //        Email = model.Email,
        //        UserName = model.Email.Split("@")[0],
        //        PhoneNumber = model.PhoneNumber,
        //    };

        //    // CreateAsync
        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (result.Succeeded is false) return BadRequest(new ApiResponse(400));

        //    return Ok(new UserDto()
        //    {
        //        DisplayName = user.DisplayName,
        //        Email = user.Email,
        //        Token = await _authService.CreateTokenAsync(user, _userManager)
        //    });

        //}





        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var user = await _userManager.FindByEmailAsync(email);
            //  Get the user's role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _authService.CreateTokenAsync(user, _userManager),
                Role = role
            });
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")]
        public async Task<ActionResult<UserAddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);
            return Ok(_mapper.Map<Core.Entities.Identity.Address, UserAddressDto>(user.Address));
        }


        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }



    }
}
