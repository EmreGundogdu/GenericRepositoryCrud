using FormulaOne.Services.Email.Interfaces;

namespace FormulaOne.Services.Email;

public class MerchService:IMerchService
{
    public void CreateMerch()
    {
        Console.WriteLine("Creating Merch");
    }

    public void RemoveMerch()
    {
        Console.WriteLine("Removing Merch");
    }
}