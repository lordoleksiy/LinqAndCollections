using CollectionsAndLinq.WebApi.Infrastructure;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfig(builder.Configuration);
builder.Services.AddMyDependencyGroup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();  // global exception handler

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
