using API.Attributes;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
       
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command , CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command , cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("{id:guid}")]
        [RedisCache]
        public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetOrdersQuery()
                                                 ,cancellationToken);
            return Ok(result);
        }
    }
}
