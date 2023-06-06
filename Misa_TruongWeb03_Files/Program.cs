using Misa_TruongWeb03.BL.Service.EmulationTitleService;
using Misa_TruongWeb03.BL.Service.FileServices;
using Misa_TruongWeb03.BL.Service.Import;
using Misa_TruongWeb03.DL.Repository.EmulationTitleRepository;
using Misa_TruongWeb03.DL.Repository.FileRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEmulationTitleImportService, EmulationTitleImportService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmulationTitleService, EmulationTitleService>();
builder.Services.AddScoped<IEmulationTitleRepository, EmulationTitleRepository>();

builder.Services.AddControllers();
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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();