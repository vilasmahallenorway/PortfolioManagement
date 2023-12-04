using AutoMapper;
using PortfolioHub.Api.Contracts.Request;
using PortfolioHub.Api.Contracts.Response;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Profiles
{
    public class UserPlantsProfile : Profile
    {
        /// <summary>
        /// Use AutoMapper to Map Entity to DTOs (Data Transfer Objects) or response objects
        /// </summary>
        public UserPlantsProfile()
        {
            CreateMap<UserPlants, UserPlantsResponseDto>().ReverseMap();
            CreateMap<UserPlants, UserPlantsCreationDto>().ReverseMap();
        }

    }
}
