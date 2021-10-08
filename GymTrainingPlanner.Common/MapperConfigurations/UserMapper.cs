namespace GymTrainingPlanner.Common.MapperConfigurations
{
    using AutoMapper;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<AppUserEntity, LoginDTO>().ReverseMap();
        }
    }
}
