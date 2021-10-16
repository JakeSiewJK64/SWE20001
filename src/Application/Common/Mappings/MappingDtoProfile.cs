using AutoMapper;
using CleanArchitecture.Application.Sales.Commands.GetSales;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class MappingDtoProfile : Profile
    {
        public MappingDtoProfile()
        {
            CreateMap<SalesRecord, SalesDto>();
        }
    }
}
