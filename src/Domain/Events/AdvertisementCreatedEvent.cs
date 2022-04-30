using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Events;
public class AdvertisementCreatedEvent : DomainEvent
{
    public AdvertisementCreatedEvent(Advertisement item)
    {
        Item = item;
    }

    public Advertisement Item { get; }
}
