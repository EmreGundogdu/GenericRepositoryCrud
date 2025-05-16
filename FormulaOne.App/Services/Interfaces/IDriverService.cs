namespace FormulaOne.App.Services.Interfaces;

public interface IDriverService
{
    Task<List<GetDriverResponse>> GetDriversAsync();
    Task<GetDriverResponse> GetDriverByIdAsync(Guid driverId);
    Task<GetDriverResponse> AddDriver(CreateDriverRequest request);
    Task<bool> UpdateDriver(UpdateDriverRequest request);
    Task<bool> DeleteDrive(Guid id);
}