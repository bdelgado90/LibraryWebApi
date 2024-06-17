using LibraryWebApi.Repositories;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services;
using LibraryWebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IDapperWrapper, DapperWrapper>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new DapperWrapper(configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
