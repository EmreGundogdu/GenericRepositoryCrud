using AutoMapper;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using MediatR;

namespace FormulaOne.API.Handlers;

public class GetDriverHandler : IRequestHandler<GetDriverQuery, GetDriverResponse>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public GetDriverHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetDriverResponse> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driver = _unitOfWork.Drivers.GetById(request.DriverId);
        return driver is null ? null : _mapper.Map<GetDriverResponse>(driver);
    }
}