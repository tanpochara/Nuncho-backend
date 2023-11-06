using System.Text;
using System.Text.Json.Serialization;
using Agoda.IoC.NetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nuncho.WebApi.database;
using Nuncho.WebApi.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
if (jwtConfig != null) builder.Services.AddSingleton<JwtConfig>(jwtConfig);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    if (jwtConfig?.Secret != null)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Customize as needed
            ValidateAudience = false, // Customize as needed
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
        };
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NunchoDatabaseContext>();

builder.Services.AutoWireAssembly(new[] {typeof(Program).Assembly}, false);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using ( var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<NunchoDatabaseContext>();
    DatabaseInitializer.Initialize(context);
}

app.Run();