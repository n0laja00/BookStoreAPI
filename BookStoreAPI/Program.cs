using BookStoreAPI.Data;
using BookStoreAPI.middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Establish Connection String to DB
builder.Services.AddDbContext<DataContext>( options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BookStoreDefault")));

//Disable automatic 400 response on requests. 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseErrorHandlingMiddleware();

//app.UseMiddleware<AntiXssMiddleware>();
app.UseAntiXssMiddleware();

app.MapControllers();

app.Run();
public partial class Program { }
