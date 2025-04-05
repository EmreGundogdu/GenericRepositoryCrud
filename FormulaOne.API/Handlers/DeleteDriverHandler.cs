using AutoMapper;
using FormulaOne.API.Commands;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using MediatR;

namespace FormulaOne.API.Handlers;

public class DeleteDriverHandler : IRequestHandler<DeleteDriverCommand, bool>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public DeleteDriverHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetById(request.DriverId);
        if (driver is null)
            return false;
        await _unitOfWork.Drivers.Delete(request.DriverId);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}