using App.Persistence.Extensions;
using App.Application.Extensions;
using CleanApp.API.Filters;
using CleanApp.API.ExceptionHandler;
using App.Application.Contracts.Caching;
using App.Caching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

builder.Services.AddScoped(typeof(NotFoundFilter<,>));
builder.Services.AddScoped<CriticalExceptionHandler>();
builder.Services.AddScoped<GlobalExceptionHandler>();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();

var app = builder.Build();

app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
