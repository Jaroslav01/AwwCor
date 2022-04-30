using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementsWithPagination;

public class AdvertisementBriefDto : IMapFrom<Advertisement>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long Amount { get; set; }
    public List<Image>? Images { get; set; }
    public DateTime Created { get; set; }
}
