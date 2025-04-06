using AutoMapper;
using FormulaOne.API.Commands;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Services.Email.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers;

[ApiController]
public class DriversController : BaseController
{
    public DriversController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
    {
    }

    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        var newDriver = await _mediator.Send(new GetDriverQuery(driverId));
        return Ok();
    }

    [HttpPost("CreateDriver")]
    public async Task<IActionResult> CreateDriver(CreateDriverRequest createDriverRequest)
    {
       
        var cmd = new CreateDriverCommand(createDriverRequest);
        var res = await _mediator.Send(cmd);
        var jobId = BackgroundJob.Enqueue<IEmailService>(x => x.SendEmail("abc@gmail.com"));
        Console.WriteLine(jobId);
        return CreatedAtAction(nameof(GetDriver), new { driverId = res.DriverId }, res);
    }

    [HttpPost("UpdateDriver")]
    public async Task<IActionResult> UpdateDriver(UpdateDriverRequest updateDriverRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var cmd = new UpdateDriverCommand(updateDriverRequest);
        var res = await _mediator.Send(cmd);
        var jobId = BackgroundJob.Schedule<IEmailService>(x => x.SendEmail("abc@gmail.com"), TimeSpan.FromSeconds(10));
        Console.WriteLine(jobId);
        return CreatedAtAction(nameof(GetDriver), new { res });
    }

    [HttpGet("GetDrivers")]
    public async Task<IActionResult> GetDrivers()
    {
        var query = new GetAllDriversQuery();
        var res = await _mediator.Send(query);

        return Ok();
    }

    [HttpDelete("DeleteDriver")]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        var cmd = new DeleteDriverCommand(driverId);
        var res = await _mediator.Send(cmd);
        RecurringJob.AddOrUpdate<IMerchService>(x=>x.DeleteMerch(Guid.NewGuid()), Cron.Minutely);
        return NoContent();
    }
}