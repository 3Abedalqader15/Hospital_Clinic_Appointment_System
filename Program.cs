using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.CustomActionFilter;
using Hospital_Clinic_Appointment_System.Repositories;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("default")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
builder.Services.AddHttpClient(); // also register the unnamed client



    // Authentication (Cookie for UI, JWT for API)
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.LogoutPath = "/Account/Logout";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
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
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = ctx =>
                {
                    // Log ctx.Exception.Message somewhere (ILogger) for debugging
                    Console.WriteLine("JWT auth failed: " + ctx.Exception.Message);
                    return Task.CompletedTask;
                },
                OnMessageReceived = ctx =>
                {
                    // Optionally inspect the raw Authorization header
                    Console.WriteLine("Auth header: " + ctx.Request.Headers.Authorization.ToString());
                    return Task.CompletedTask;
                }
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
        var defaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder(
            CookieAuthenticationDefaults.AuthenticationScheme,
            JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        options.DefaultPolicy = defaultPolicy;

        options.AddPolicy("AdminOnly", policy => {
            policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireRole("Admin");
        });
        options.AddPolicy("DoctorOnly", policy => {
            policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireRole("Doctor");
        });
        options.AddPolicy("PatientOnly", policy => {
            policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireRole("Patient");
        });
        options.AddPolicy("DoctorOrAdmin", policy => {
            policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireRole("Doctor", "Admin");
        });
        options.AddPolicy("PatientOrAdmin", policy => {
            policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireRole("Patient", "Admin");
        });
    });



    // Repositories
    builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
    builder.Services.AddScoped<IPatientRepository, PatientRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
    builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    builder.Services.AddScoped<IAuthRepository, AuthRepository>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IApiClient, ApiClient>();



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
