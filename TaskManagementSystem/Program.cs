using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Repositories;
using TaskManagement.Infrastructure.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using TaskManagement.Infrastructure.Services;
using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Infrastructure.Repositories.Infrastructure.Repositories;
using Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Commands.Tasks;
using TaskManagement.Application.Queries.Tasks;
using TaskManagement.Application.Commands.Teams;
using TaskManagement.Application.Commands.Users;
using TaskManagement.Application.Queries.Teams;
using TaskManagement.Application.Queries.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));
//builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Task Handlers
builder.Services.AddScoped<CreateTaskCommandHandler>();
builder.Services.AddScoped<UpdateTaskCommandHandler>();
builder.Services.AddScoped<UpdateTaskStatusCommandHandler>();
builder.Services.AddScoped<DeleteTaskCommandHandler>();

builder.Services.AddScoped<GetTaskByIdQueryHandler>();
builder.Services.AddScoped<GetTasksByUserQueryHandler>();
builder.Services.AddScoped<GetAllTasksQueryHandler>();

// User Handlers
builder.Services.AddScoped<CreateUserCommandHandler>();
builder.Services.AddScoped<UpdateUserCommandHandler>();
builder.Services.AddScoped<DeleteUserCommandHandler>();
builder.Services.AddScoped<GetUserByIdQueryHandler>();
builder.Services.AddScoped<GetUserByEmailQueryHandler>();
builder.Services.AddScoped<GetAllUsersQueryHandler>();

// Team Handlers
builder.Services.AddScoped<CreateTeamCommandHandler>();
builder.Services.AddScoped<UpdateTeamCommandHandler>();
builder.Services.AddScoped<DeleteTeamCommandHandler>();
builder.Services.AddScoped<GetTeamByIdQueryHandler>();
builder.Services.AddScoped<GetAllTeamsQueryHandler>();



builder.Services.AddSwaggerGen();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Host.UseSerilog((context, services, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration)
//        .WriteTo.File("logs/request.log", rollingInterval: RollingInterval.Day));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole("Admin", "Manager"));
//});

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
