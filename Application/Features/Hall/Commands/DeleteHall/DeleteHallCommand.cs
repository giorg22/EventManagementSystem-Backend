using MediatR;

public record DeleteHallCommand(int Id) : IRequest<Unit>;