using AutoMapper;
using LoadFit.APIs.DTOs;
using LoadFit.APIs.Errors;
using LoadFit.Core;
using LoadFit.Core.Entities;
using LoadFit.Core.Entities.Identity;
using LoadFit.Core.Repositories.Contract;
using LoadFit.Core.Specifications;
using LoadFit.Core.Specifications.VehicleSpecifications;
using LoadFit.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoadFit.APIs.Controllers
{

    public class VehiclesController : BaseApiController
    {
        private readonly IGenericRepository<Vehicle> _vehicleRepo;
        private readonly IGenericRepository<VehicleBrand> _brandsRepo;
        private readonly IGenericRepository<VehicleType> _vehicleTypesRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketRepository _basketRepository;

        public VehiclesController(IGenericRepository<Vehicle> vehicleRepo,
            IGenericRepository<VehicleBrand> brandsRepo,
            IGenericRepository<VehicleType> vehicleTypesRepo,
            IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IBasketRepository basketRepository)
        {
            _vehicleRepo = vehicleRepo;
            _brandsRepo = brandsRepo;
            _vehicleTypesRepo = vehicleTypesRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _basketRepository = basketRepository;
        }

        // BaseUrl/api/vehicles
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [ProducesResponseType(typeof(IEnumerable<VehicleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<VehicleDto>>> GetVehicles(string? sort, int? brandId, int? typeId, string? search, string basketId)
        {
            // Step 1: Fetch the user's basket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null || basket.Items == null || !basket.Items.Any())
            {
                return BadRequest(new ApiResponse(400, "No items found in the basket"));
            }

            // Step 2: Compute total weight of the items
            var totalWeight = basket.Items.Sum(item => item.Weight);

            var maxItemLength = basket.Items.Max(item => item.Length);
            var maxItemWidth = basket.Items.Max(item => item.Width);
            var maxItemHeight = basket.Items.Max(item => item.Height);

            var spec = new VehicleWithBrandAndTypeSpecifications(sort, brandId, typeId, search);
            var vehicles = await _vehicleRepo.GetAllWithSpecAsync(spec);
            var vehicleDtos = _mapper.Map<IReadOnlyList<Vehicle>, IReadOnlyList<VehicleDto>>(vehicles);

            VehicleDto? bestVehicle = vehicleDtos
            .Where(v => v.MaxWeight >= totalWeight &&
                    v.Length >= maxItemLength &&
                    v.Width >= maxItemWidth &&
                    v.Height >= maxItemHeight)
            .OrderBy(v => v.price)
            .FirstOrDefault();


            foreach (var vehicleDto in vehicleDtos)
            {
                vehicleDto.IsRecommended = (bestVehicle != null && vehicleDto.Id == bestVehicle.Id);

                if (vehicleDto.DriverId.HasValue)
                {
                    var driver = await _unitOfWork.Repository<Driver>().GetAsync(vehicleDto.DriverId.Value);
                    if (driver != null)
                    {
                        var user = await _userManager.FindByIdAsync(driver.UserId);
                        vehicleDto.DriverName = user?.DisplayName;
                    }
                }
            }

            // Step 6: Sort vehicles so that the recommended one appears first
            vehicleDtos = vehicleDtos.OrderByDescending(v => v.IsRecommended).ToList();

            return Ok(vehicleDtos);
        }

        // BaseUrl/api/vehicles/1
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDto>> GetVehicle(int id)
        {
            var spec = new VehicleWithBrandAndTypeSpecifications(id);
            var vehicle = await _vehicleRepo.GetWithSpecAsync(spec);
            if (vehicle == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var vehicleDto = _mapper.Map<Vehicle, VehicleDto>(vehicle);

            if (vehicle.Driver != null)
            {
                vehicleDto.DriverId = vehicle.Driver.Id;

                // Fetch user by Driver.UserId
                var user = await _userManager.FindByIdAsync(vehicle.Driver.UserId);
                vehicleDto.DriverName = user?.DisplayName;
            }

            return Ok(vehicleDto);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<VehicleBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetTypes()
        {
            var types = await _vehicleTypesRepo.GetAllAsync();
            return Ok(types);
        }


    }
}
