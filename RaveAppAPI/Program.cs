using RaveAppAPI.Services.Repository;
using RaveAppAPI.Services.Repository.Contracts;
var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}



var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseExceptionHandler(
//    new ExceptionHandlerOptions()
//    {
//        AllowStatusCode404Response = true, // important!
//        ExceptionHandlingPath = "/error"
//    });
app.UseHttpsRedirection();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.Run();
