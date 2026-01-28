using Hospital_Clinic_Appointment_System.App_Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
