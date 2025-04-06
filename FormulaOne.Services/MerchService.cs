using FormulaOne.Services.Email.Interfaces;

namespace FormulaOne.Services;

public class MerchService:IMerchService
{
    public void CreateMerch(Guid driverId)
    {
        Console.WriteLine("Creating Merch");
    }

    public void DeleteMerch(Guid merchId)
    {
        Console.WriteLine("Deleting Merch");
    }
}