using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Simon_TopStyle.Core.Authentications;
using Simon_TopStyle.Core.Interfaces;
using Simon_TopStyle.Core.Security;
using Simon_TopStyle.Core.Services;
using Simon_TopStyle.Data.DataModels;
using Simon_TopStyle.Data.Interfaces;
using Simon_TopStyle.Data.Repos;
using Simon_TopStyle.Models.Users;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {jwtSecurityScheme, Array.Empty<string>() }
    });
});
builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IRolesRepo, RolesRepo>();
builder.Services.AddScoped<IRolesService, RolesService>();
if (builder.Environment.IsProduction())
{
    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
    var clientId = builder.Configuration.GetSection("KeyVault:ClientId");
    var clientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    var directoryId = builder.Configuration.GetSection("KeyVault:DirectoryId");
    var credential = new ClientSecretCredential(directoryId.Value!.ToString()
                                                    , clientId.Value!.ToString()
                                                    , clientSecret.Value!.ToString());
    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString()
                                            , clientId.Value.ToString()
                                            , clientSecret.Value!.ToString()
                                            , new DefaultKeyVaultSecretManager());
    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);
    builder.Services.AddDbContext<TopStyleDBContext>(opt =>
                                opt.UseSqlServer(client.GetSecret("VaultConnString").Value.Value.ToString()));
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TopStyleDBContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("connString"))
        );
}

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<TopStyleDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("JwtConfig:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value))
        };
    });






var app = builder.Build();


app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api Doc"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());



app.Run();
