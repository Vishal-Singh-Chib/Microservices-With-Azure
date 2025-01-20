using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.DataAccess;
using Wpm.Clinic.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ManagementService>();


var app = builder.Build();
builder.Services.AddDbContext<ClinicDbContext>(options =>
{
    options.UseInMemoryDatabase("WpmClinic");

});
builder.Services.AddHttpClient<ManagementService>(client =>
{
    var uri = builder.Configuration.GetValue<string>("Wpm__ManagemntUri");
    client.BaseAddress = new Uri(uri);

});
app.EnsureDbISCreated();
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
