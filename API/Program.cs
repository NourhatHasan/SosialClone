

using API;
using Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.Activities;
using RepositoryAplication.SecretInterfaces;
using RepositoryAplication.SecretInterfaces.security;
using RepositoryAplication.Tools;
using sosialClone;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//adding a authorization policy to the hole app
builder.Services.AddControllers(opts=>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser().Build();

    //every controller endPoint will require authentication
    opts.Filters.Add(new AuthorizeFilter(policy));
}
);  


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Context 
builder.Services.AddDbContext<DataContext>(
    opts =>
    {
        opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionAPIConeectionString"), b => b.MigrationsAssembly("API"));
    });



//Identity
builder.Services.AddIdentityServices(builder.Configuration);


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


//userInfo
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<userInterface, userAccesor>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


//seeding the data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    //pass the datacontext and userManager to seed method
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<user>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);

}
catch(Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "error accored during seeding");
}






app.Run();
