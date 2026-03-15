using LibraryApi.Data;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .Select(e => new { field = e.Key, error = e.Value!.Errors.First().ErrorMessage })
                .ToList();

            return new UnprocessableEntityObjectResult(new
            {
                status = 422,
                message = "Erro de Validação",
                errors
            });
        };
    });

builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IAutorService, AutorService>();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    var url = builder.Configuration["DATASOURCE_URL"];
    var username = builder.Configuration["DATASOURCE_USERNAME"];
    var password = builder.Configuration["DATASOURCE_PASSWORD"];

    options.UseNpgsql($"Host={url};Database=library;Username={username};Password={password}");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
    

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
