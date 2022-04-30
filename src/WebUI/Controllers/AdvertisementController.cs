using CleanArchitecture.Application.Advertisements.Commands.CreateAdvertisement;
using CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementByIdQuery;
using CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementsWithPagination;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AdvertisementController : ApiControllerBase
{
    [HttpPost("CreateAd")]
    public async Task<int> CreateAd(CreateAdvertisementCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("GetAdvertisementsWithPagination")]
    public async Task<PaginatedList<AdvertisementBriefDto>> GetAdvertisementsWithPagination(GetAdvertisementsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut("GetAdvertisementById")]
    public async Task<object> GetAdvertisementById(GetAdvertisementByIdQuery query)
    {
        return await Mediator.Send(query);
    }
}
