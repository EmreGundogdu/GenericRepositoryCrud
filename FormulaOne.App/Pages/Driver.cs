using FormulaOne.App.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FormulaOne.App.Pages;

public partial class Driver
{
    [Inject]
    private IDriverService _driverService {get;set;}
    
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    
    [Parameter]public string driverId { get; set; }

    public GetDriverResponse driverModel { get; set; } = new();

    public string Message { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(driverId))
        {
            var _driverId = new Guid(driverId);
            var driver = await _driverService.GetDriverByIdAsync(_driverId);
            if (driver != null)
            {
                driverModel = driver;
            }
        }
    }

    protected void HandleInvalidSubmit()
    {
        Message = "Something went wrong";
    }

    protected async Task HandleValidSubmit()
    {
        if (string.IsNullOrEmpty(driverId))
        {
            var newDriver = new CreateDriverRequest()
            {
                DriverNumber = driverModel.DriverNumber,
                FirstName = driverModel.FullName,
                LastName = driverModel.FullName,
                DateOfBirth = driverModel.DateOfBirth,
            };
            var response = await _driverService.AddDriver(newDriver);
            if(response!=null)
                _navigationManager.NavigateTo("/drivers");
            else
                Message = "Something went wrong, please try again";
        }
        else
        {
            var updateDriverRequest = new UpdateDriverRequest()
            {
                DriverId = driverModel.DriverId,
                FirstName = driverModel.FullName,
                LastName = driverModel.FullName,
                DateOfBirth = driverModel.DateOfBirth,
            };
            var response = await _driverService.UpdateDriver(updateDriverRequest);
            if (response!=null)
                _navigationManager.NavigateTo("/drivers");  
            
            Message = "Something went wrong, please try again";
        }
    }

    protected async Task DeleteDriver()
    {
        if (!string.IsNullOrEmpty(driverId))
        {
            var _driverId = new Guid(driverId);
            var res = await _driverService.DeleteDrive(_driverId);
            if (res!=null)
                _navigationManager.NavigateTo("/drivers");  
            
            Message = "Something went wrong, please try again";
        }
    }
}