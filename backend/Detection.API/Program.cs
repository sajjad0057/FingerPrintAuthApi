using System.Text;
using Detection.API.Data;
using Detection.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configure DbContext with SQLite
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Apply migrations automatically
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Register the UserService for dependency injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IFingerPrintService, FingerPrintService>();


//// Configure JWT Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = configuration["Jwt:Issuer"],
//            ValidAudience = configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
//        };
//    });

//builder.Services.AddAuthorization();


#region ForConfiguring CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSites",
        builder =>
        {
            builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
        });
});

#endregion


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

////For configuring CORS
app.UseCors("AllowSites");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();






