using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoMachine.Components;
using TodoMachine.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddMassTransit(config  => 
//{
//    config.AddConsumer<SubmitTodoConsumer>();
    
//    config.AddRequestClient<ISubmitTodo>();
//});

builder.Services.AddMediator(config => 
{
    config.AddConsumer<SubmitTodoConsumer>();

    config.AddRequestClient<ISubmitTodo>();
});

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
