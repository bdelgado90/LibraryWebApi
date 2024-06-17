using LibraryWebApi.Repositories;
using LibraryWebApi.Repositories.Interfaces;
using LibraryWebApi.Services;
using LibraryWebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Library");
builder.Services.AddTransient<IDapperWrapper>(_ => new DapperWrapper(connectionString));
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
