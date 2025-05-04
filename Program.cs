var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Add services to the container. The default is to use the Microsoft.AspNetCore.Mvc.NewtonsoftJson package for JSON serialization.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();     // Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen();   // Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddHttpClient(); // Register IHttpClientFactory for dependency injection + Needed for controller MovieController to work correctly.


var app = builder.Build();  // Configure the HTTP request pipeline. The default is to use the Microsoft.AspNetCore.Mvc.NewtonsoftJson package for JSON serialization.

// Configure the HTTP request pipeline for development and production environments
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection(); // Enable HTTPS redirection + This is the default behavior in ASP.NET Core applications.
app.MapGet("/", () => "API is running!");

app.UseAuthorization(); // Enable authorization middleware + This is the default behavior in ASP.NET Core applications.

app.MapControllers(); // Map attribute-routed controllers to the request pipeline + This is the default behavior in ASP.NET Core applications.

app.Run();
