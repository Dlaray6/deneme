using EduTrack.Application.Interfaces;
using EduTrack.Application.Services;
using EduTrack.Data;
using EduTrack.Data.Repositories;
using EduTrack.Data.Repositories.Interfaces;
using EduTrack.Data.Repositories.Services;
using EduTrack.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<EduTrackDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,               // Maksimum tekrar deneme sayýsý
                maxRetryDelay: TimeSpan.FromSeconds(10),  // Tekrar denemeler arasýndaki maksimum gecikme
                errorNumbersToAdd: null         // Opsiyonel, özel SQL hata numaralarý eklenebilir
            );
        }
    ));


// ===== DEPENDENCY INJECTION - REPOSITORIES & SERVICES =====
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IClassLessonRepository, ClassLessonRepository>();
builder.Services.AddScoped<ITeacherClassLessonRepository, TeacherClassLessonRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IClassLessonService, ClassLessonService>();
builder.Services.AddScoped<ITeacherClassLessonService, TeacherClassLessonService>();
builder.Services.AddScoped<JwtService>();

// ===== CORS CONFIGURATION =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7033") // Blazor Client portunu yaz
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ===== JWT AUTHENTICATION =====
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

// ===== MVC & API CONFIGURATION =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EduTrack API", Version = "v1" });

    // JWT için Swagger Security Definition
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// ===== CONFIGURE PIPELINE =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduTrack API V1");
    });
}

app.UseHttpsRedirection();

// CORS middleware burada ve sadece bir kere olmalý
app.UseCors("AllowBlazorClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
