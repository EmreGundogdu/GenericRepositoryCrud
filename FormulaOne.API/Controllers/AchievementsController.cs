using AutoMapper;
using FormulaOne.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers;

public class AchievementsController : BaseController
{
    public AchievementsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
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
}