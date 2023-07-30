using WebScraper.Application.SeedWorks;
using WebScraper.DataAccess.SeedWorks;

string Policy = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy(Policy, p => p.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationLayerEntryPoint).Assembly));
builder.Services.AddMongo();
builder.Services.AddCustomServices();

builder.Services.Configure<BackgroundServicesSetting>(options => builder.Configuration.GetSection("BackgroundServicesSetting").Bind(options));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
