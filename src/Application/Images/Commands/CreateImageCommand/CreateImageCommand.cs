using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Images.Commands.CreateImageCommand;

public class CreateImageCommand : IRequest<int>
{
    public int AdvertisementId { get; set; }
    public string Url { get; set; }
}

public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var entity = new Image
        { 
            AdvertisementId = request.AdvertisementId,
            Url = request.Url
        };


        _context.Images.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
