using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class Advertisement : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long Amount { get; set; }
    public List<Image> Images { get; set; } = null!;

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}

public class AdvertisementAbbreviated
{
    public string Title { get; set; }
    public long Amount { get; set; }
    public Image Image { get; set; }
}