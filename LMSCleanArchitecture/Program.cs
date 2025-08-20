using AutoMapper;
using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses;
using LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetById;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using LMSCleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<LMSCleanArchitecrure.Application.Contracts.Persistance.IInstructorRepository,
    LMSCleanArchitecture.Infrastructure.Repositories.InstructorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddDbContext<LMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR handlers from relevant assemblies
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(CreateCourseCommand).Assembly,
        typeof(GetAllCoursesQuery).Assembly,
        typeof(GetInstructorByIdQuery).Assembly
    ));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Optional: Add authentication if needed
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Optional: Log startup exceptions
try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Startup failed: {ex.Message}");
}
