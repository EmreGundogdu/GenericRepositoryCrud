using FormulaOne.API.Services.Interfaces;
using FormulaOne.Entities.Contracts;
using MassTransit;

namespace FormulaOne.API.Services;

public class DriverNotificationPublisherService:IDriverNotificationPublisherService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public DriverNotificationPublisherService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    public async Task SendNotification(Guid driverId, string teamName)
    {
        await _publishEndpoint.Publish(new DriverNotificationRecord(driverId, teamName));
    }
}