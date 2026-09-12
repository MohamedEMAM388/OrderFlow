using Application.Features.Orders.Queries.GetOrdersDashboard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class DashboardController(IMediator mediator) : ControllerBase
{

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrdersDashboard(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrdersDashboardQuery(), cancellationToken);
        return Ok(result);
    }

}