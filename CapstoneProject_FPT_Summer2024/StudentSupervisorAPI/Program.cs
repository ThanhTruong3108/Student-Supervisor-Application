using CloudinaryDotNet;
using Domain.Enums.Role;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StudentSupervisorAPI.Cofiguration;
using StudentSupervisorService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add DI Services
builder.Services.AddDIServices(builder.Configuration);

builder.Services.AddControllers();
ServiceConfigurations.ConfigureSwagger(builder.Services);
ServiceConfigurations.ConfigureAuthentication(builder.Services, builder.Configuration);

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
