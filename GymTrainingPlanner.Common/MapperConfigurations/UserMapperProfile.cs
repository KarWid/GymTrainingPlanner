﻿namespace GymTrainingPlanner.Common.MapperConfigurations
{
    using AutoMapper;
    using GymTrainingPlanner.Common.Models.Dtos.V1.Account;
    using GymTrainingPlanner.Repositories.EntityFramework.Entities;

    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterAccountRequest, AppUserEntity>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(_ => _.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(_ => _.Email));

            CreateMap<AppUserEntity, RegisterAccountResponse>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(_ => _.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(_ => _.Id));

            CreateMap<AppUserEntity, UserAccount>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => _.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(_ => _.Email));
        }
    }
}
