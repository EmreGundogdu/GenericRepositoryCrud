using AutoMapper;
using FormulaOne.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.API.Controllers;

[Route("api/[controller]")]
public class BaseController:ControllerBase
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;   
        _mapper = mapper;
    }
}