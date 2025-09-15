using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RaveAppAPI.Services.Helpers;
using RaveAppAPI.Services.Repository;
using RaveAppAPI.Services.Repository.Contracts;
using System.Text;
try
{
    Logger.LogInfo("Starting...");
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    builder.Services.AddScoped<INoticiaService, NoticiaService>();
    builder.Services.AddScoped<IEventoService, EventoService>();
    builder.Services.AddScoped<IHealthService, HealthService>();
    builder.Services.AddScoped<IFiestaService, FiestaService>();
    builder.Services.AddScoped<IEntradaService, EntradaService>();
    builder.Services.AddScoped<IArtistaService, ArtistaService>();
    builder.Services.AddScoped<IMediaService, MediaService>();
    builder.Services.AddScoped<IReseniaService, ReseniaService>();
    builder.Services.AddScoped<IPagoService, PagoService>();
    builder.Services.AddScoped<ILogService, LogService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IReporteService, ReporteService>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Enter 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

    builder.Configuration.AddEnvironmentVariables("RAVEAPP_");

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            RequireExpirationTime = false,
            RequireAudience = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"]))
        });
    builder.Services.AddAuthorization();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Allow",
            policy => policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
    });

    var app = builder.Build();
    app.UseCors("Allow");
    app.UseAuthentication();
    app.UseAuthorization();
    // Configure the HTTP request pipeline.

    //app.UseExceptionHandler(
    //    new ExceptionHandlerOptions()
    //    {
    //        AllowStatusCode404Response = true, // important!
    //        ExceptionHandlingPath = "/error"
    //    });
    app.UseHttpsRedirection();
    app.MapControllers();
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}
    Logger.LogInfo("Ready to run...");
    app.Run();

}
catch (Exception e)
{
    Logger.LogError(e.Message);
}
