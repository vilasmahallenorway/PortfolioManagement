using AutoMapper;
using PortfolioHub.Api.Contracts.Request;
using PortfolioHub.Api.Contracts.Response;
using PortfolioHub.Domain.Models;

namespace PortfolioHub.Api.Profiles
{
    public class TenantProfile : Profile
    {
        /// <summary>
        /// Use AutoMapper to Map Entity to DTOs (Data Transfer Objects) or response objects
        /// </summary>
        public TenantProfile()
        {
            CreateMap<Tenants, TenantResponseDto>().ReverseMap();
            CreateMap<Tenants, TenentCreationDto>().ReverseMap();
            CreateMap<Tenants, TenantUpdationDto>().ReverseMap();

        }
    }
}
