using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque.GetById;
using MarmorariaProjeto.UseCases.ItemEstoque.Register;
using MarmorariaProjeto.UseCases.ItemEstoque.GetAll;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MarmorariaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<RegisterItemEstoqueUseCase>();
builder.Services.AddScoped<GetItemEstoqueByIdUseCase>();
builder.Services.AddScoped<GetItemEstoqueAllUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevelopment", policy =>
    {
        policy.WithOrigins(
                "https://localhost:5001",
                "http://localhost:5000",
                "http://localhost:5001")
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
app.UseAuthorization();
app.MapControllers();

app.Run();