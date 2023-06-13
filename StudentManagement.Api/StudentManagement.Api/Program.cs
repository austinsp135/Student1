using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Persistence;
using StudentManagement.Repository;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);
var dbContext = ConfigureDbContext(builder.Services,builder.Configuration);


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

dbContext.Database.Migrate();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
}

StudentDbContext ConfigureDbContext(IServiceCollection services,ConfigurationManager configuration)
{
    var dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>().UseNpgsql(configuration.GetConnectionString("PostgreSQL")).Options;
    var dbContext = new StudentDbContext(dbContextOptions);
    services.AddSingleton(dbContext);
    return dbContext;
}
