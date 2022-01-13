using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDos.MinimalAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IToDoService, ToDoService>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ToDoValidator));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/todos", (IToDoService service) => service.GetAll());
//app.MapGet("/todos", ToDoRequests.GetAll);
//app.MapGet("/todos/{id}", ([FromServices]IToDoService service, [FromRoute]Guid id) => service.GetById(id));
//app.MapGet("/todos/{id}", ToDoRequests.GetById);
//app.MapPost("/todos", (IToDoService service, [FromBody]ToDo toDo) => service.Create(toDo));
//app.MapPost("/todos", ToDoRequests.Create);
//app.MapPut("/todos/{id}", (IToDoService service, [FromRoute]Guid id, [FromBody]ToDo toDo) => service.Update(toDo));
//app.MapPut("/todos/{id}", ToDoRequests.Update);
//app.MapDelete("/todos/{id}", (IToDoService service, Guid id) => service.Delete(id));
//app.MapDelete("/todos/{id}", ToDoRequests.Delete);
app.RegisterEndpoints();
app.Run();
