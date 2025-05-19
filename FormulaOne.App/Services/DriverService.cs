using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FormulaOne.App.Services.Interfaces;

namespace FormulaOne.App.Services;

public class DriverService:IDriverService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public DriverService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            
        };
    }
    
    public async  Task<List<GetDriverResponse>> GetDriversAsync()
    {
        try
        {
            var response = await _httpClient.GetStreamAsync("api/drivers/GetDrivers");
            var drivers = await JsonSerializer.DeserializeAsync<List<GetDriverResponse>>(response, _jsonSerializerOptions);
            return drivers;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GetDriverResponse> GetDriverByIdAsync(Guid driverId)
    {
        try
        {
            var driver = await _httpClient.GetFromJsonAsync<GetDriverResponse>($"api/drivers/{driverId}");
            return driver;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<GetDriverResponse> AddDriver(CreateDriverRequest request)
    {
        try
        {
            var driverJson = new StringContent(JsonSerializer.Serialize(request),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/drivers", driverJson);
            if (!response.IsSuccessStatusCode)
                return null;
            
            var responseBody = await response.Content.ReadAsStreamAsync();
            var newDriver = await JsonSerializer.DeserializeAsync<GetDriverResponse>(responseBody, _jsonSerializerOptions);
            return newDriver;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async  Task<bool> UpdateDriver(UpdateDriverRequest request)
    {
        try
        {
            var driverJson = new StringContent(JsonSerializer.Serialize(request),Encoding.UTF8,"application/json");
            var response = await _httpClient.PutAsync("api/drivers", driverJson);
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }   
    }

    public async Task<bool> DeleteDrive(Guid id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/drivers/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}