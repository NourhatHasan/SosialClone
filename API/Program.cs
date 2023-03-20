

using API;
using Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.Activities;
using RepositoryAplication.Tools;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Context 
builder.Services.AddDbContext<DataContext>(
    opts =>
    {
        opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionAPIConeectionString"), b => b.MigrationsAssembly("API"));
    });

//connect  to brouser

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().
     AllowAnyHeader());
});

/*
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000/");
    });
});
*/


//fluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Create>();



//mediatR
builder.Services.AddMediatR(typeof(list.handler));


//autoMapper
builder.Services.AddAutoMapper(typeof(ProfileMapper).Assembly);
var app = builder.Build();



//midleWare need to be above swagger
app.UseMiddleware<ExcaptionMidleware>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//seeding the data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await Seed.SeedData(context);

}
catch(Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "error accored during seeding");
}






app.Run();
