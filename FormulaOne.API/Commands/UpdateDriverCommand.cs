using MediatR;

namespace FormulaOne.API.Commands;
public class UpdateDriverCommand : IRequest<bool>
{
    public UpdateDriverRequest UpdateDriverRequest { get; }

    public UpdateDriverCommand(UpdateDriverRequest updateDriverRequest)
    {
        UpdateDriverRequest = updateDriverRequest;
    }
}