using Mapster;
using SGULibraryBE.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi()
                .AllowCORS()
                .RegisterDbContext(builder.Configuration)
                .RegisterServices()
                .RegisterRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting(); 
app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.ConfigureRequestMapper();
app.Run();
