using AutoMapper;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers;

public class AchievementsController : BaseController
{
    public AchievementsController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : base(unitOfWork, mapper, mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetDriverAchievement([FromQuery] Guid driverId)
    {
        var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementsAsync(driverId);
        if (driverAchievements == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<DriverAchievementResponse>(driverAchievements));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAchievement([FromBody] CreateDriverAchievementRequest createDriverAchievementRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var res = _mapper.Map<Achievement>(createDriverAchievementRequest);
        await _unitOfWork.Achievements.Add(res);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetDriverAchievement), new { driverId = res.DriverId }, res);

    }

    [HttpPost]
    public async Task<IActionResult> CreateAchievement([FromBody] UpdateDriverAchievementRequest updateDriverAchievementRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var res = _mapper.Map<Achievement>(updateDriverAchievementRequest);
        await _unitOfWork.Achievements.Update(res);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetDriverAchievement), new { driverId = res.DriverId }, res);

    }
}