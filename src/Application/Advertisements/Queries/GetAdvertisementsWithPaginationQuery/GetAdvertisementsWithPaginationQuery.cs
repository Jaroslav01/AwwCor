using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementsWithPagination;
public class GetAdvertisementsWithPaginationQuery : IRequest<PaginatedList<AdvertisementBriefDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public EOrderByType OrderBy { get; set; } = EOrderByType.CreatedDate;
    public EOrderType OrderType { get; set; } = EOrderType.Increase;
}

public class GetAdvertisementsWithPaginationQueryHandler : IRequestHandler<GetAdvertisementsWithPaginationQuery, PaginatedList<AdvertisementBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAdvertisementsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AdvertisementBriefDto>> Handle(GetAdvertisementsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<AdvertisementBriefDto>? advertisements = null;
        if (request.OrderBy == EOrderByType.CreatedDate && request.OrderType == EOrderType.Increase)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderBy(x => x.Created)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.OrderBy == EOrderByType.CreatedDate && request.OrderType == EOrderType.Descending)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderByDescending(x => x.Created)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if (request.OrderBy == EOrderByType.Amount && request.OrderType == EOrderType.Increase)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderByDescending(x => x.Amount)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else if(request.OrderBy == EOrderByType.Amount && request.OrderType == EOrderType.Descending)
        {
            advertisements = await _context.Advertisements
                        .AsNoTracking()
                        .OrderByDescending(x => x.Amount)
                        .ProjectTo<AdvertisementBriefDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        foreach (var item in advertisements.Items)
        {
            List<Image> images = new();
            if(item.Images?.Count >= 1) images.Add(item.Images[0]);
            item.Images = images;
        }
        return advertisements;
    }
}
