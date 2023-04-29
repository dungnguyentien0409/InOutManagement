using Interfaces;
using DataAccessEF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Implementations;
using MinimalApi.Endpoint.Configurations.Extensions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using UserApi.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Configuration.AddConfigurationFile("appsettings.json");
builder.Services.AddDbContext<InOutManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfile(new DtosToViewModelsMappingProfile());
    mc.AddProfile(new EntitiesToDtosMappingProfile());
}).CreateMapper());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
            };
        });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<InOutManagementContext>();
    Console.WriteLine("User API connect db: " + context.Database.CanConnect());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

