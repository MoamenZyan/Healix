using Healix.Infrastructure.IoC;
using Healix.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Healix Api Documentation");
        options.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.Urls.Add("http://0.0.0.0:5136");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
