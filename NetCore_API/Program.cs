using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        $"C:/webLogs/NetCore_API/{DateTime.Now.Date:dd-mm-yyyy}.log",
        restrictedToMinimumLevel: LogEventLevel.Information,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3}] {Username} {Message:lj}{NewLine}{Exception}"
        )
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog();

builder.Services.AddHealthChecks();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbService(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging(); // <-- Add this line

app.MapHealthChecks("/healthz").RequireHost("*:7107"); ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
