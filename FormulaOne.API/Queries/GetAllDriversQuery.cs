using FormulaOne.Entities.DbSet;
using MediatR;

namespace FormulaOne.API.Queries;

public class GetAllDriversQuery:IRequest<IEnumerable<GetDriverResponse>>
{
    
}