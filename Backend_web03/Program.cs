using Misa_TruongWeb03.BL.Service.EmulationCommendationService;
using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.BL.Service.Import;
using Misa_TruongWeb03.DL.Repository.EmulationCommendationRepository;
using Misa_TruongWeb03.DL.Repository.EmulationTitleRepository;
using Misa_TruongWeb03.DL.Repository.FileRepository;
using Misa_TruongWeb03.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEmulationTitleService,EmulationTitleService>();
builder.Services.AddScoped<IEmulationTitleRepository,EmulationTitleRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

builder.Services.AddScoped<IEmulationCommendationService, EmulationCommendationService>();
builder.Services.AddScoped<IEmulationCommendationRepository, EmulationCommendationRepository>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
