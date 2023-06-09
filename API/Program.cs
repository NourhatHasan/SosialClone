

using API;
using API.SignalR;
using Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RepositoryAplication.Activities;
using RepositoryAplication.photo;
using RepositoryAplication.SecretInterfaces;
using RepositoryAplication.SecretInterfaces.security;
using RepositoryAplication.Tools;
using SecretPro.Photos;
using sosialClone;
using static SecretPro.security.sHostRequirement;

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

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options
     .SetIsOriginAllowed(_ => true) // Allow any origin
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials());
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
//custom authorization policy
builder.Services.AddAuthorization(opts =>
opts.AddPolicy("IsHost", policy =>
{
   policy.Requirements.Add(new IsHostRequirement());
})
);

//we want that the policy to last as long as the method is running
builder.Services.AddTransient<IAuthorizationHandler, IsHostReuirementHandler>();




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






//fluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Create>();



//mediatR
builder.Services.AddMediatR(typeof(list.handler));


//autoMapper
builder.Services.AddAutoMapper(typeof(ProfileMapper).Assembly);


//userInfo
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserAccesor, UserAccesor>();

//Cloudinary settings
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<IPhotoAccoesor, photoAccesor>();

//SignalR
builder.Services.AddSignalR();

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

//create the root to connect to chatHub
app.MapHub<ChatHub>("/chat");




//seeding the data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    //pass the datacontext and userManager to seed method
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);

}
catch(Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "error accored during seeding");
}






app.Run();
