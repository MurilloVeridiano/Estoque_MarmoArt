using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MarmorariaProjeto.Domain.Entities;
using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.Infrastructure.Security;
using MarmorariaProjeto.UseCases.ItemEstoque;
using MarmorariaProjeto.UseCases.Movimentacao;

var builder = WebApplication.CreateBuilder(args);

if (string.IsNullOrWhiteSpace(builder.Configuration["JwtSettings:SigningKey"]))
{
    if (!builder.Environment.IsDevelopment())
    {
        throw new InvalidOperationException("Configure JwtSettings:SigningKey com uma chave secreta de pelo menos 32 bytes.");
    }

    builder.Configuration["JwtSettings:SigningKey"] = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
    builder.Logging.AddConsole();
}

builder.Services.AddDbContext<MarmorariaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<RegisterItemEstoqueUseCase>();
builder.Services.AddScoped<GetItemEstoqueByIdUseCase>();
builder.Services.AddScoped<GetItemEstoqueAllUseCase>();
builder.Services.AddScoped<UpdateItemEstoqueUseCase>();
builder.Services.AddScoped<DeleteItemEstoqueUseCase>();
builder.Services.AddScoped<RegisterMovimentacaoUseCase>();
builder.Services.AddScoped<GetMovimentacaoById>();
builder.Services.AddScoped<GetMovimentacaoAllUseCase>();
builder.Services.AddScoped<UpdateMovimentacaoUseCase>();
builder.Services.AddScoped<DeleteMovimentacaoUseCase>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IPasswordHasher<Login>, PasswordHasher<Login>>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var signingKey = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SigningKey"]!);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(signingKey),
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevelopment", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5000",
                "https://localhost:5001",
                "http://localhost:5500",
                "http://127.0.0.1:5500",
                "http://localhost:5173",
                "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("LocalDevelopment");
app.UseAuthentication();
app.UseAuthorization();

var frontendPath = Path.Combine(builder.Environment.ContentRootPath, "FrontEnd");
var frontendProvider = new PhysicalFileProvider(frontendPath);
app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = frontendProvider });
app.UseStaticFiles(new StaticFileOptions { FileProvider = frontendProvider });
app.MapControllers();

app.Run();