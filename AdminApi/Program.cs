﻿using System.Text;
using AutoMapper;
using DataAccessEF;
using DataAccessEF.UnitOfWork;
using Domain.Interfaces;
using AdminApi.Interfaces;
using AdminApi.Implementations;
using AdminApi.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Endpoint.Configurations.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Configuration.AddConfigurationFile("appsettings.json");
builder.Services.AddDbContext<InOutManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfile(new DtosToViewModelsMappingProfile());
    mc.AddProfile(new EntitiesToDtosMappingProfile());
}).CreateMapper());
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IDoorRoleService, DoorRoleService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    Console.WriteLine("Admin API: " + context.Database.GetDbConnection().ConnectionString);
    Console.WriteLine("Admin API connect db: " + context.Database.CanConnect());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

