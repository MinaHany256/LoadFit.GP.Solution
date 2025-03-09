using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using LoadFit.APIs.DTOs;
using LoadFit.Core.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace LoadFit.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Vehicle, VehicleDto, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductPictureUrlResolver(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //public string Resolve(Vehicle source, VehicleDto destination, string destMember, ResolutionContext context)
        //{
        //    if (!string.IsNullOrEmpty(source.PictureUrl))
        //    {
        //        return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
        //    }

        //    return string.Empty ;

        //}

        public string Resolve(Vehicle source, VehicleDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = $"{request?.Scheme}://{request?.Host.Value}";

                return $"{baseUrl}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
