using AutoMapper;
using FormulaOne.API.Commands;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> CreateDriver(CreateDriverRequest createDriverRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var cmd = new CreateDriverCommand(createDriverRequest);
        var res = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetDriver), new { driverId = res.DriverId }, res);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDriver(UpdateDriverRequest updateDriverRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var cmd = new UpdateDriverCommand(updateDriverRequest);
        var res = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetDriver), new { res });
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var query = new GetAllDriversQuery();
        var res = await _mediator.Send(query);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        var cmd = new DeleteDriverCommand(driverId);
        var res = await _mediator.Send(cmd);
        return NoContent();
    }
}