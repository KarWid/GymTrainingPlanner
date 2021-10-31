namespace GymTrainingPlanner.Common.MapperConfigurations
{
    using AutoMapper;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterAccountInDTO, AppUserEntity>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(_ => _.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(_ => _.Email));

            CreateMap<AppUserEntity, RegisterAccountOutDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(_ => _.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(_ => _.Id));
        }
    }
}
