
using AutoMapper;
using GenerateUnitTestsWithAi.API.Mapper;
using GenerateUnitTestsWithAi.API.Services;
using GenerateUnitTestsWithAi.API.Services.Configuration;

namespace GenerateUnitTestsWithAi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ITransformationService, TransformationService>();
            builder.Services.AddScoped<ICsvWriterService, CsvWriterService>();
            builder.Services.AddScoped<IAIGeneratorService, RapidApiChatGptService>();
            builder.Services.AddScoped<IUnitTestService, UnitTestService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<CsvOptions>(
                builder.Configuration.GetSection(CsvOptions.SectionKey));

            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceToApiMappingProfile>();
                // Add more profiles as needed
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
