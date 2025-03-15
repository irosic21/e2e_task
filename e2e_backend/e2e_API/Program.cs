using e2e_DAL;
using e2e_DAL.Repositories.Interfaces;
using e2e_services.Services.Interfaces;
using e2e_services.Services;
using Microsoft.EntityFrameworkCore;
using e2e_DAL.Repositories;
using e2e_API.Helpers;
using e2e_services.Helpers;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500")
                   .AllowAnyMethod() 
                   .AllowAnyHeader() 
                   .AllowCredentials(); 
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<DateTime>(() => new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string", Format = "date" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ILeaveRecordsRepository, LeaveRecordsRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

app.UseCors("AllowFrontend");

string baseDirectory = builder.Configuration.GetValue<string>("FileStorage:BaseDirectory");
if (string.IsNullOrEmpty(baseDirectory))
{
    throw new Exception("Critical error: BaseDirectory is not set in appsettings.json. Server cannot start.");
}
Console.WriteLine($"Base Directory: {baseDirectory}");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(baseDirectory, "pictures")),
    RequestPath = "/pictures",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS");
        ctx.Context.Response.Headers.Add("Access-Control-Allow-Headers", "*");

    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
