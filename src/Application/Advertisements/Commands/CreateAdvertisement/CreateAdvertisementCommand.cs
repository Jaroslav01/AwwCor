using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Images.Commands.CreateImageCommand;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.Advertisements.Commands.CreateAdvertisement;
public class CreateAdvertisementCommand : IRequest<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long Amount { get; set; }
    public List<string> ImagesUrl { get; set; } = null!;
}

public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _mediator;

    public CreateAdvertisementCommandHandler(IApplicationDbContext context, ISender mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        var entity = new Advertisement()
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
        };

        entity.DomainEvents.Add(new AdvertisementCreatedEvent(entity));

        _context.Advertisements.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        foreach (var imageUrl in request.ImagesUrl)
        {
            await _mediator.Send(new CreateImageCommand() { AdvertisementId = entity.Id, Url = imageUrl }, cancellationToken);
        }
        return entity.Id;
    }
}
