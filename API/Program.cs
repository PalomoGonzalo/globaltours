
using System.Text;
using System.Net;
using API.Helpers;
using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Core.Entidades;
using API.Controllers;
using Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

/// configuracion para heroku



var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

builder.WebHost.UseKestrel()
    .ConfigureKestrel((context,options)=>
    {
        options.Listen(IPAddress.Any, Int32.Parse(port),listenOptions=>
        {

        });
        
    });

Console.WriteLine("puerto heroku:" +port);



var connectionString= builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options=>
                                                        options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILugarRepositorio, LugarRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio,CategoriaRepositorio>();
builder.Services.AddScoped<IPaisRepositorio,PaisRepositorio>();
builder.Services.AddScoped(typeof(IRepositorioGenerico<>),(typeof(Repositorio<>)));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddCors( x => x.AddPolicy("EnableCors", builder => {
                builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyOrigin()
                        //.WithOrigins("https://codestack.com")
                        .AllowAnyMethod()
                        //.WithMethods("PATCH", "DELETE", "GET", "HEADER")
                        .AllowAnyHeader();
                        //.WithHeaders("X-Token", "content-type")
            }));




// Creo los servicios para jwt 
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretKey"));
builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x=>{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey= true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});







var app = builder.Build();

// Aplicar las nuevas migraciones al ejecutar la aplicacion
using(var scope=app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    var loggerFactory=services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await BaseDatosSeed.SeedAsync(context,loggerFactory);
    }
    catch (System.Exception ex)
    {
        
        var logger=loggerFactory.CreateLogger<Program>();
        logger.LogError(ex,"Un error ocurrio durante la migracion");
    }

}

// Configure the HTTP request pipeline.

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseSwagger();
app.UseSwaggerUI();


/// redireccion de control error
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseCors("EnableCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
