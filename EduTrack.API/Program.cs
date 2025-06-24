using EduTrack.Application.Interfaces;
using EduTrack.Application.Services;
using EduTrack.Data;
using EduTrack.Data.Repositories;
using EduTrack.Data.Repositories.Interfaces;
using EduTrack.Data.Repositories.Services;
using EduTrack.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<IClassLessonRepository, ClassLessonRepository>();
builder.Services.AddScoped<IClassLessonService, ClassLessonService>();
builder.Services.AddScoped<ITeacherClassLessonRepository, TeacherClassLessonRepository>();
builder.Services.AddScoped<ITeacherClassLessonService, TeacherClassLessonService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddScoped<JwtService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
  // .AddNegotiate();

builder.Services.AddDbContext<EduTrackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Bu önce olacak!
app.UseAuthorization();  // Bu sonra


app.MapControllers();

app.Run();
