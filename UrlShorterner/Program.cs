using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = "server=localhost;user=root;password=root;database=urlShorter";
var serverVersion = new MySqlServerVersion(new Version(8, 4, 6));

builder.Services.AddDbContext<ApplicationDbContext>(dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion));


var app = builder.Build();



app.MapControllers();
app.UseHttpsRedirection();
app.Run();


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Response> Urls { get; set; }
}



public class Response
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Short { get; set; } = string.Empty;
    public string CreateAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
}
