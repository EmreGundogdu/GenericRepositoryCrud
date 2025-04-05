using AutoMapper;
using FormulaOne.API.Commands;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using MediatR;

namespace FormulaOne.API.Handlers;

public class UpdateDriverHandler : IRequestHandler<UpdateDriverCommand, bool>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public UpdateDriverHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Driver>(request.UpdateDriverRequest);
        await _unitOfWork.Drivers.Update(data);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}