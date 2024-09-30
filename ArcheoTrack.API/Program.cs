using ArcheoTrack.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseSqlite( $"Filename={Path.Combine( FileSystem.CurDir(), "notes.db" )}" ) );

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors( builder =>
       builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader() );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
