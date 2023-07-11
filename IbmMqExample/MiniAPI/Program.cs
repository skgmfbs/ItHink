using Microsoft.EntityFrameworkCore;
using MiniAPI.Data;
using MiniAPI.DTOs;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDB"))
);
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IToDoStateRepository, ToDoStateRepository>();

builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IToDoStateService, ToDoStateService>();
builder.Services.AddScoped<IPublishingService, PublishingService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/todos", async (IToDoRepository repository) =>
{
    var items = await repository.GetAllAsync();
    return Results.Ok(items);
});

app.MapGet("api/todos/{id}", async (IToDoRepository repository, int id) =>
{
    var item = await repository.GetByIdAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(item);
});

app.MapPost("api/todos", async (IToDoService service, ToDoDto toDo) =>
{
    var result = await service.CreateAsync(toDo);
    if (result.Id > 0)
    {
        return Results.Created($"api/todos/{result.Id}", result);
    }
    return Results.UnprocessableEntity(toDo);
});

app.MapPost("api/todos/state", async (IToDoStateService service, ToDoStateDto toDoState) =>
{
    var result = await service.AddAsync(toDoState);
    if (result.Id > 0)
    {
        return Results.Ok();
    }

    return Results.UnprocessableEntity(toDoState);
});

app.Run();