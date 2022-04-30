using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Images.Commands.CreateImageCommand;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Advertisements.EventHandlers;

public class AdvertisementCreatedEventHandler : INotificationHandler<DomainEventNotification<AdvertisementCreatedEvent>>
{
    private readonly ILogger<AdvertisementCreatedEventHandler> _logger;
    private readonly ISender _mediator = null!;

    public AdvertisementCreatedEventHandler(ILogger<AdvertisementCreatedEventHandler> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public Task Handle(DomainEventNotification<AdvertisementCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);
            
        return Task.CompletedTask;
    }
}