using System.Text.Json;
using FormulaOne.Entities.Contracts;
using MassTransit;

namespace Fia.Api.Services;

public class DriverNotificationConsumer:IConsumer<DriverNotificationRecord>
{
    private readonly ILogger<DriverNotificationConsumer> _logger;

    public DriverNotificationConsumer(ILogger<DriverNotificationConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<DriverNotificationRecord> context)
    {
        _logger.LogInformation("Driver notification received: {@context}", context.Message.driverId + " - " + context.Message.driverName);
    }
}