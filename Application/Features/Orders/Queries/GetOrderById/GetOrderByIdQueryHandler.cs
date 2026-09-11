using Application.Contracts;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork ,
    IMapper mapper) : IRequestHandler<GetOrderByIdQuery , OrderToReturnDto>
{
    public async Task<OrderToReturnDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            throw new NotFoundException
                ($"The order with {request.OrderId} Not Found");
        
        // map from order to orderToReturnDto
        return mapper.Map<OrderToReturnDto>(order);
        
    }
}