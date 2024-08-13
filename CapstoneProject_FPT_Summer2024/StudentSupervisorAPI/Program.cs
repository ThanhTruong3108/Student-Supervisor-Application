using Coravel;
using Microsoft.AspNetCore.Builder;
using Net.payOS;
using StudentSupervisorAPI.Cofiguration;
using StudentSupervisorService;
using StudentSupervisorService.Service.Implement;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));

var builder = WebApplication.CreateBuilder(args);

// Add DI Services
builder.Services.AddScheduler(); // tạo cron job
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddSingleton(payOS);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


builder.Services.AddControllers();
ServiceConfigurations.ConfigureSwagger(builder.Services);
ServiceConfigurations.ConfigureAuthentication(builder.Services, builder.Configuration);

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

// đặt thời gian chạy cron job
// thiết lập múi giờ ở Việt Nam để chạy cron job
TimeZoneInfo vietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<DailyScheduleImplement>().DailyAtHour(7).Zoned(vietNamTimeZone); // chạy vào 7h sáng
    scheduler.Schedule<DailyScheduleImplement>().DailyAtHour(19).Zoned(vietNamTimeZone); // chạy vào 19h tối

    scheduler.Schedule<WeeklyScheduleImplement>().Weekly().Sunday().Zoned(vietNamTimeZone); // chạy vào mỗi chủ nhật hàng tuần
});

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
