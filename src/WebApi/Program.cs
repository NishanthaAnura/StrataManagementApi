using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureAppServices();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

//seed roles and admin user
await app.SeedRolesAndAdminUser();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
