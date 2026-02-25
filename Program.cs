using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.CustomActionFilter;
using Hospital_Clinic_Appointment_System.Repositories;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers(options =>
{
   
    options.Filters.Add<ValidateModelAttribute>();
});
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




    //  JWT Authentication
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                ),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

    //  Session
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    //  Authorization Policies
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
        options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));
        options.AddPolicy("DoctorOrAdmin", policy => policy.RequireRole("Doctor", "Admin"));
        options.AddPolicy("PatientOrAdmin", policy => policy.RequireRole("Patient", "Admin"));
    });



    // Repositories
    builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
    builder.Services.AddScoped<IPatientRepository, PatientRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
    builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    builder.Services.AddScoped<IAuthRepository, AuthRepository>();
    builder.Services.AddScoped<IJwtService, JwtService>();



    // Database
    builder.Services.AddDbContext<DBContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("HospitalConnectionString"),

            sqlOptions => sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            )
            )
        );

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    if (!app.Environment.IsDevelopment())
    {

        app.UseExceptionHandler("/Error");

        app.UseHsts();
    }


    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseSession();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapStaticAssets();
    app.MapRazorPages()
       .WithStaticAssets();

    app.MapControllers();

    if (!app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();

