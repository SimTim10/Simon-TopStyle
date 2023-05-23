using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Models.Users;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TopStyleDBContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("connString"))
        );
}

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TopStyleDBContext>()
    .AddDefaultTokenProviders();




var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.MapControllers();


app.Run();
