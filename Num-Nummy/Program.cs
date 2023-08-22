using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using Num_Nummy.Db;
using Num_Nummy.Tools;
using NumClass.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options
     .SetIsOriginAllowed(_ => true) // Allow any origin
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials());
});


   builder.Services.AddDbContext<Context>(
        opts =>
        {
            opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionAPIConeectionString"), b => b.MigrationsAssembly("Num-Nummy"));
        });




//IdentityServices
builder.Services.AddIdentityServices(builder.Configuration);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


//seeding the data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    //pass the datacontext and userManager to seed method
    var context = services.GetRequiredService<Context>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);

}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "error accored during seeding");
}
app.MapControllers();




app.Run();
