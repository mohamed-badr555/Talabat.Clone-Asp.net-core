using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{
    
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost] // api/Orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderdto)
        {
            var address = _mapper.Map<AddressDTo, Address>(orderdto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(orderdto.BuyerEmail, orderdto.BasketId,orderdto.DeliveryMethodId, address);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(order));
            
        }
        [HttpGet] //Get : /api/Orders?mohammedb.555dr@gmail.com
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
        {
            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{Id}")] //Get : /api/Orders/Id?mohammedb.555dr@gmail.com 
        public async Task<ActionResult<OrderToReturnDto>> GetOrderforUser(int id,string email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, email);
            if (order is null) return BadRequest(new ApiResponse(404));

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

            return Ok(deliveryMethods);
        }

    }
}
