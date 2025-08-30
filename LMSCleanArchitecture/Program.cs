using AutoMapper;
using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses;
using LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetById;
using LMSCleanArchitecrure.Application.Features.Student.Command.CreateStudent;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using LMSCleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Log the connection string for debugging
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"[DEBUG] Using connection string: {connectionString}");

// Add Identity
builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<LMSDbContext>()
.AddSignInManager<SignInManager<IdentityUser>>();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// DbContext
builder.Services.AddDbContext<LMSDbContext>(opt =>
    opt.UseSqlServer(connectionString));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<LMSCleanArchitecrure.Application.Contracts.Persistance.IInstructorRepository,
    LMSCleanArchitecrure.Infrastructure.Repositories.InstructorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddHttpContextAccessor();

// Register MediatR handlers from relevant assemblies
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(CreateCourseCommand).Assembly,
        typeof(GetAllCoursesQuery).Assembly,
        typeof(GetInstructorByIdQuery).Assembly,
        typeof(CreateStudentCommand).Assembly
    ));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LMS API", Version = "v1" });

    // Add JWT Bearer
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Ensure database is created and seed roles/admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<LMSDbContext>();
    await db.Database.MigrateAsync();

    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();

    foreach (var role in new[] { "Admin", "Student", "Instructor" })
    {
        if (!await roleMgr.RoleExistsAsync(role))
        {
            await roleMgr.CreateAsync(new IdentityRole(role));
            Console.WriteLine($"[SEED] Created role: {role}");
        }
        else
        {
            Console.WriteLine($"[SEED] Role already exists: {role}");
        }
    }

    var adminEmail = builder.Configuration["Admin:Email"] ?? "adminlms";
    var admin = await userMgr.FindByEmailAsync(adminEmail);
    if (admin is null)
    {
        admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var pwd = builder.Configuration["Admin:Password"] ?? "Admin1234";
        var res = await userMgr.CreateAsync(admin, pwd);
        if (res.Succeeded)
        {
            await userMgr.AddToRoleAsync(admin, "Admin");
            Console.WriteLine("[SEED] Created admin user and assigned Admin role.");
        }
        else
        {
            Console.WriteLine("[SEED ERROR] Failed to create admin user: " + string.Join(", ", res.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        Console.WriteLine("[SEED] Admin user already exists.");
        if (!await userMgr.IsInRoleAsync(admin, "Admin"))
        {
            await userMgr.AddToRoleAsync(admin, "Admin");
            Console.WriteLine("[SEED] Assigned Admin role to existing admin user.");
        }
    }
}


// Enable Developer Exception Page in Development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Startup failed: {ex.Message}");
}
