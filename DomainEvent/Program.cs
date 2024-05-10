using DomainEvent.Core;
using DomainEvent.Core.Event.Event;
using DomainEvent.Services;
using DomainEvent.Services.EventHandler;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DomainEventContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PersonServices>();

builder.Services.AddTransient<IDomainEventHandler<Createdperson>,WriteCreatePersonToConsole>();
builder.Services.AddTransient<IDomainEventDispatcher,DomainEventDispatcher>();


var app = builder.Build();
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