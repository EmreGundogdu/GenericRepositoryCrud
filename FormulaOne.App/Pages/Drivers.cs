using FormulaOne.App.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FormulaOne.App.Pages;

public partial class Drivers
{
    [Inject]
    private IDriverService _driverService { get; set; }
    
    public IEnumerable<GetDriverResponse> _drivers {get; set;} = new List<GetDriverResponse>();

    protected async override Task OnInitializedAsync()
    {
        var drivers = await _driverService.GetDriversAsync();
        if(drivers.Any())
            _drivers = drivers;
    }
}