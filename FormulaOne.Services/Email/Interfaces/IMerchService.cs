namespace FormulaOne.Services.Email.Interfaces;

public interface IMerchService
{
     void CreateMerch(Guid driverId);
     void DeleteMerch(Guid merchId);
}