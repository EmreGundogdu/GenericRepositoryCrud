using AutoMapper;
using FormulaOne.Entities.DbSet;

public class MappingProfile : Profile
{


    public MappingProfile()
    {
        CreateMap<CreateDriverAchievementRequest, Achievement>()
            .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
            .ForMember(dest => dest.WorlChampionship, opt => opt.MapFrom(src => src.WorldChampionship))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.FastestLap, opt => opt.MapFrom(src => src.FastestLap))
            .ForMember(dest => dest.PolePosition, opt => opt.MapFrom(src => src.PolePosition))
            .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins));

        CreateMap<UpdateDriverAchievementRequest, Achievement>()
        .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins));

        CreateMap<Achievement, DriverAchievementResponse>()
        .ForMember(dest => dest.Wins, opt => opt.MapFrom(src => src.RaceWins));

        CreateMap<Driver, CreateDriverRequest>()
      .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
      .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
      .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
      .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(src => src.DriverNumber));

        CreateMap<Driver, UpdateDriverRequest>()
     .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
     .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
     .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
     .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(src => src.DriverNumber));

        CreateMap<Driver, GetDriverResponse>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
        .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(src => src.DriverNumber))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth));
    }
}