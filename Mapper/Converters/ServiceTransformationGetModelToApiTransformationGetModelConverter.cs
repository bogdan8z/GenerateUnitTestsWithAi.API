using AutoMapper;

namespace GenerateUnitTestsWithAi.API.Mapper.Converters
{
    public class ServiceTransformationGetModelToApiTransformationGetModelConverter : ITypeConverter<Services.Models.TransformationGetModel, Models.TransformationGetModel>
    {
        public Models.TransformationGetModel Convert(Services.Models.TransformationGetModel source, Models.TransformationGetModel destination, ResolutionContext context)
        {
            return new Models.TransformationGetModel
            {
                String = source.Key,
                Code = source.Value,
            };
        }
    }
}
