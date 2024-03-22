using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using reas.Helpers;
using reas.Model;
using reas.Services;
using repository;
using service;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
// Mail Service
var emailConfig = configuration
	.GetSection("EmailConfiguration")
	.Get<EmailConfiguration>();

builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().Count().SetMaxTop(100).Expand().Filter());
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder
	.Services
	.AddSingleton(emailConfig);

builder
	.Services
	.AddScoped<IEMailSenderService, EMailSenderService>();

// Entity Service
builder
   .Services
   .AddScoped<ITokenService, TokenService>()
   .AddScoped<IUserRepository, UserRepository>()
   .AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddScoped<IDepositService, DepositService>();
builder.Services.AddScoped<IBidService, BidService>();

builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IDepositRepository, DepositRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>()
				.AddScoped<INewsService, NewsService>();






// Adding Authentication
builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	// Adding Jwt Bearer
	// https://stackoverflow.com/questions/43045035/jwt-token-authentication-expired-tokens-still-working-net-core-web-api
	.AddJwtBearer(options =>
	{
		options.SaveToken = true;
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true, // Ki?m tra th?i gian s?ng c?a token
			ClockSkew = TimeSpan.Zero, // Kh�ng s? d?ng token slide
			ValidAudience = configuration["JWT:ValidAudience"],
			ValidIssuer = configuration["JWT:ValidIssuer"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
		};
	});

builder.Services.AddControllers()
	.AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "reas", Version = "v1" });
	// Custom swagger default schema sample
	c.SchemaFilter<SwaggerCustomSchemaFilter>();
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."

	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				new string[] {}
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowSpecificOrigin");
//}

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
