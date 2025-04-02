using AutoMapper;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers;

public class DriversController : BaseController
{
    public DriversController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        var driver = _unitOfWork.Drivers.GetById(driverId);
        if (driver is null)
            return NotFound();
        return Ok(_mapper.Map<GetDriverResponse>(driver));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDriver(CreateDriverRequest createDriverRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var data = _mapper.Map<Driver>(createDriverRequest);
        await _unitOfWork.Drivers.Add(data);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetDriver), new { driverId = data.Id }, res);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDriver(UpdateDriverRequest updateDriverRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var data = _mapper.Map<Driver>(updateDriverRequest);
        await _unitOfWork.Drivers.Update(data);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetDriver), new { driverId = data.Id });
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        return Ok(_mapper.Map<IEnumerable<GetDriverResponse>>(_unitOfWork.Drivers.GetAll()));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        var driver = await _unitOfWork.Drivers.GetById(driverId);
        if (driver is null)
            return NotFound();
        await _unitOfWork.Drivers.Delete(driverId);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}