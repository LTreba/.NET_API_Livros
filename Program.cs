using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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
