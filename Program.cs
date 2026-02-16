using BEPersonal.DBs;
using BEPersonal.Mappers;
using BEPersonal.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

Env.Load(); // legge .env nella root

var builder = WebApplication.CreateBuilder(args);

var SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
var key = Environment.GetEnvironmentVariable("JWT_SECRET");
var expireHours = Environment.GetEnvironmentVariable("JWT_EXPIRE_HOURS");

// Add services to the container.
builder.Services.AddDbContext<PersonalDBContext>(options =>
    options.UseSqlServer(SqlConnectionString!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<MessageMappers>();
    cfg.AddProfile<UserMappers>();
});
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
