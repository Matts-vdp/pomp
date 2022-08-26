using PompServer.Services;
using PompServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<PumpService>();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("corsapp",
//        policy =>
//        {
//        policy.AllowAnyHeader()
//        .AllowAnyMethod()
//        .WithOrigins(new string[] { "http://localhost:5000" , "http://localhost:3000" })
//            .AllowCredentials();
//        });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
//app.UseCors("corsapp");
app.UseAuthorization();
app.MapControllers();
app.MapHub<UpdateHub>("/updatehub");
app.Run();
