public class UpdateDriverRequest
{
    public Guid DriverId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int DriverNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}