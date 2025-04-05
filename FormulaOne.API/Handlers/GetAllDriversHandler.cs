using AutoMapper;
using FormulaOne.API.Queries;
using FormulaOne.Data.Repositories.Interfaces;
using MediatR;

namespace FormulaOne.API.Handlers;

public class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<GetDriverResponse>>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public GetAllDriversHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetDriverResponse>> Handle(GetAllDriversQuery request,
        CancellationToken cancellationToken)
    {
        var alldrivers = _unitOfWork.Drivers.GetAll();
        return _mapper.Map<IEnumerable<GetDriverResponse>>(alldrivers);
    }
}