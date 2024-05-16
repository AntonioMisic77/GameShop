using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Configurations;
using ProdavaonicaIgaraAPI.Middlewares;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;
using ProdavaonicaIgaraAPI.Repositories.GenericRepository;
using ProdavaonicaIgaraAPI.Services;
using ProdavaonicaIgaraAPI.Services.Receipt;
using ProdavaonicaIgaraAPI.Services.ReceiptItem;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");

builder.Services.AddDbContext<PIGDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IReceiptRepository, ReceiptRepository>();
builder.Services.AddTransient<IReceiptItemRepository, ReceiptItemRepository>();

builder.Services.AddTransient<IArticleService, ArticleService>();
builder.Services.AddTransient<ISupplierService, SupplierService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IReceiptService, ReceiptService>();
builder.Services.AddTransient<IReceiptItemService, ReceiptItemService>();

builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
