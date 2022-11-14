using System.Text;
using DatingApp.API.Data;
using DatingApp.API.Data.Repositories;
using DatingApp.API.Data.Seed;
using DatingApp.API.Profiles;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services =builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");
services.AddControllersWithViews();
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

// Replace 'YourDbContext' with the name of your own DbContext derived class.
services.AddDbContext<DataContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()));


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddScoped<ITokenService,TokenService>();
services.AddScoped<IMemberService,MemberService>();
services.AddScoped<IUserRepository,UserRepository>();
services.AddScoped<IAuthService,AuthService>();
services.AddAutoMapper(typeof(UserMapperProfile).Assembly);


services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });

var app = builder.Build();

// Add data into database
using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try {
    var context = serviceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    Seed.SeedUser(context);
}
catch(Exception ex){
    var logger= serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"Migrate Failed");
}
//end add data

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
