using Scracth2025.DATABASE;
using Scratch2025.API;

var builder = WebApplication.CreateBuilder(args);

var dbOptions = builder.Configuration.BindOptions<DbOptions>("Database");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!builder.Environment.IsTest())
{
	builder.Services.AddDb(dbOptions);
	AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
