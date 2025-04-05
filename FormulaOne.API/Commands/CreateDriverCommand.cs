using MediatR;

namespace FormulaOne.API.Commands;
public class CreateDriverCommand : IRequest<GetDriverResponse>
{
    public CreateDriverRequest CreateDriverRequest { get; }

    public CreateDriverCommand(CreateDriverRequest createDriverRequest)
    {
        CreateDriverRequest = createDriverRequest;
    }
}