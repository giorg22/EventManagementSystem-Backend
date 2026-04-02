using MediatR;

public record GetHallByIdQuery(int Id) : IRequest<HallDto?>;