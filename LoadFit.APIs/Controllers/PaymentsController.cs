using AutoMapper;
using LoadFit.APIs.DTOs;
using LoadFit.APIs.Errors;
using LoadFit.Core.Services.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoadFit.APIs.Controllers
{

    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        //// Create or update Payment Intent
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        //[HttpPost]
        //public async Task<ActionResult<OrderToReturnDto>> CreateOrUpdatePaymentIntent(string basketId, string buyerEmail)
        //{
        //    var order = await _paymentService.CreateOrUpdatePaymentIntent(basketId, buyerEmail);
        //    if (order == null) return BadRequest(new ApiResponse(400));

        //    var mappedOrder = _mapper.Map<OrderToReturnDto>(order);
        //    return Ok(mappedOrder);
        //}

    }
}
