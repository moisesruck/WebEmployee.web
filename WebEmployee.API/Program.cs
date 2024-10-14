using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebEmployee.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// Adds the ApplicationDbContext to the service collection for dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    // Configures the context to use SQL Server with the specified connection string
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// Adds response caching middleware to the service collection
builder.Services.AddResponseCaching();

// Retrieves a secret key from the configuration (usually for JWT authentication)
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

// Configures authentication for the application
builder.Services.AddAuthentication(x =>
{
    // Sets the default authentication and challenge scheme to JWT Bearer
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Configures JWT Bearer token authentication
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Disables HTTPS requirement for local testing
    x.SaveToken = true; // Saves the token on successful authentication for reuse
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // Validates the signing key of the token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), // Sets the signing key used to validate the token
        ValidateIssuer = false, // Disables issuer validation (should enable for production)
        ValidateAudience = false // Disables audience validation (should enable for production)
    };
});

// Adds the controller service with cache profiles, JSON serialization, and XML serialization support
builder.Services.AddControllers(option =>
{
    // Adds a cache profile named "Default30" with a cache duration of 30 seconds
    option.CacheProfiles.Add("Default30",
       new CacheProfile()
       {
           Duration = 30
       });
})
// Adds Newtonsoft.Json for JSON serialization/deserialization
.AddNewtonsoftJson()
// Adds support for XML serialization using Data Contract formatters
.AddXmlDataContractSerializerFormatters();

// Configures CORS (Cross-Origin Resource Sharing) to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin() // Allows requests from any origin
            .AllowAnyMethod() // Allows any HTTP method (GET, POST, etc.)
            .AllowAnyHeader(); // Allows any header to be sent
        });
});

// Adds controller services again (redundant in this case, as it was already added above)
builder.Services.AddControllers();

// Configures Swagger/OpenAPI for API documentation generation
builder.Services.AddSwaggerGen(options =>
{
    // Adds a security definition for Bearer tokens in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    // Adds security requirements for Bearer token authentication in Swagger
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>() // No specific scopes required
        }
    });
});

// Adds the endpoint API explorer service for Swagger
builder.Services.AddEndpointsApiExplorer();
// Adds the Swagger generation services (duplicate; was already added above)
builder.Services.AddSwaggerGen();


// Builds the application pipeline with the configured services
var app = builder.Build();

// Configures Swagger for use in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger middleware
    app.UseSwaggerUI(); // Enables the Swagger UI
}

// Adds middleware to redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Adds the authentication middleware to the request pipeline
app.UseAuthentication();

// Adds the authorization middleware to the request pipeline
app.UseAuthorization();

// Maps controller endpoints to the request pipeline
app.MapControllers();

// Adds the configured CORS policy to the request pipeline
app.UseCors("AllowAll");

// Adds HTTPS redirection again (this line is redundant since it’s already configured above)
app.UseHttpsRedirection();

// Runs the application
app.Run();
