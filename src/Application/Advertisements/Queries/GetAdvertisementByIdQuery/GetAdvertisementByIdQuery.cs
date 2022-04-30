using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementByIdQuery;
public class GetAdvertisementByIdQuery : IRequest<object>
{
    public int Id { get; set; }
    public bool AllFields { get; set; } = false;
}
public class GetAdvertisementByIdQueryHandler : IRequestHandler<GetAdvertisementByIdQuery, object>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAdvertisementByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<object> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.AllFields)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(x => x.Id == request.Id);
            if (advertisement != null) advertisement.Images = _context.Images.Where(x => x.AdvertisementId == advertisement.Id).ToList();
            return advertisement;
        }
        else
        {
            return _context.Advertisements
                .Where(x => x.Id == request.Id)
                .Select(x => new AdvertisementAbbreviated
                {
                    Title = x.Title,
                    Amount = x.Amount,
                    Image = x.Images[0]
                });
        }
    }
}