using Misa_TruongWeb03.BL.Service.Base;
using Misa_TruongWeb03.BL.Service.EmisStudy.AnswerService;
using Misa_TruongWeb03.BL.Service.EmisStudy.ExerciseService;
using Misa_TruongWeb03.BL.Service.EmisStudy.GradeService;
using Misa_TruongWeb03.BL.Service.EmisStudy.QuestionService;
using Misa_TruongWeb03.BL.Service.EmisStudy.SubjectService;
using Misa_TruongWeb03.BL.Service.EmisStudy.TopicService;
using Misa_TruongWeb03.Common.DTO.EmisStudy;
using Misa_TruongWeb03.Common.Entity.EmisStudy.Answer;
using Misa_TruongWeb03.DL.Repository.EmisStudy.AnswerRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.ExerciseRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.GradeReposiotry;
using Misa_TruongWeb03.DL.Repository.EmisStudy.QuestionRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.SubjectRepo;
using Misa_TruongWeb03.DL.Repository.EmisStudy.TopicRepo;
using Misa_TruongWeb03.Emis.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IGradeService, GradeService>();

builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IAnswerService, AnswerService>();

builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
