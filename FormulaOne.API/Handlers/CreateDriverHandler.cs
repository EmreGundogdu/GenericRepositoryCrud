using AutoMapper;
using FormulaOne.API.Commands;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using MediatR;

namespace FormulaOne.API.Handlers;

public class CreateDriverHandler : IRequestHandler<CreateDriverCommand, GetDriverResponse>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public CreateDriverHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetDriverResponse> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Driver>(request.CreateDriverRequest);
        await _unitOfWork.Drivers.Add(data);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<GetDriverResponse>(data);
    }
}