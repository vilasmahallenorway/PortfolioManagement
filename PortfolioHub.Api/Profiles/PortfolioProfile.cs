using AutoMapper;
using PortfolioHub.Api.Contracts.Request;
using PortfolioHub.Api.Contracts.Response;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Profiles
{
    public class PortfolioProfile : Profile
    {
        /// <summary>
        /// Use AutoMapper to Map Entity to DTOs (Data Transfer Objects) or response objects
        /// </summary>
        public PortfolioProfile()
        {
            CreateMap<Portfolios, PortfolioResponseDto>().ReverseMap();
            CreateMap<Portfolios, PortfolioCreationDto>().ReverseMap();
        }

    }
}
