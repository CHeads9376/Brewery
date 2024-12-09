using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using.bits.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<bits>(options =>

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Test connection 
// Test the MySQL connection

TestDatabaseConnection(builder.Configuration);



// Middleware setup

app.UseHttpsRedirection();

app.UseStaticFiles();



app.UseRouting();



app.UseAuthorization();



app.MapControllerRoute(

    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();



// Method to test the database connection

void TestDatabaseConnection(IConfiguration configuration)

{

    // Retrieve the connection string from appsettings.json

    string connectionString = configuration.GetConnectionString("MySqlConnection");



    using (var connection = new MySqlConnection(connectionString))

    {

        try

        {

            connection.Open();

            Console.WriteLine("Database connection successful!");

        }

        catch (Exception ex)

        {

            Console.WriteLine($"Database connection error: {ex.Message}");

        }

    }

}



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
