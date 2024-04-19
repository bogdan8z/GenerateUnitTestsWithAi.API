using AutoMapper;
using GenerateUnitTestsWithAi.API.Mapper.Converters;

namespace GenerateUnitTestsWithAi.API.Mapper
{
    public class ServiceToApiMappingProfile : Profile
    {
        public ServiceToApiMappingProfile()
        {
            CreateMap<Services.Models.TransformationGetModel, Models.TransformationGetModel>().ConvertUsing<ServiceTransformationGetModelToApiTransformationGetModelConverter>();
        }
    }
}
