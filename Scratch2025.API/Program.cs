using Scracth2025.DATABASE;
using Scratch2025.API;
using Scratch2025.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISecretManagerService, SecretManagerService>();

//scratch-2025-local-connection-string

//var dbOptions = builder.Configuration.BindOptions<DbOptions>("Database");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var tempSecretManager = new SecretManagerService(builder.Configuration);

if (!builder.Environment.IsTest())
{
	builder.Services.AddDb(new DbOptions
	{
		ConnectionString = await tempSecretManager.GetSecretAsync("scratch-2025-local-connection-string")
	});
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
