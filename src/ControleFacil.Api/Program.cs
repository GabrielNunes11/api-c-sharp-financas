using System.Text;
using AutoMapper;
using ControleFacil.Api.AutoMapper;
// using ControleFacil.Api.Contract.NaturezaDeLancamento;
using ControleFacil.Api.Domain.Repository.Class;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Class;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ControleFacil.Api.DTO.NatureReleaseDTO;
using ControleFacil.Api.DTO.ToPayDTO;
using ControleFacil.Api.DTO.ToReceiveDTO;

var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration(builder);

ConfigureDependencyInjection(builder);

var app = builder.Build();

AppConfiguration(app);

app.Run();

static void ConfigureDependencyInjection(WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration.GetConnectionString("DEFAULT");

    builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseNpgsql(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

    var config = new MapperConfiguration(cfg => {
        cfg.AddProfile<UserProfileMapper>();
        cfg.AddProfile<NatureReleaseProfileMapper>();
        cfg.AddProfile<ToPayProfileMapper>();
        cfg.AddProfile<ToReceiveProfileMapper>();
    });

    IMapper mapper = config.CreateMapper();

    builder.Services
        .AddSingleton(builder.Configuration)
        .AddSingleton(builder.Environment)
        .AddSingleton(mapper)
        .AddScoped<TokenService>()
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<INatureReleaseRepository, NatureReleaseRepository>()
        .AddScoped<IService<NatureReleaseRequestDTO, NatureReleaseResponseDTO, Guid>, NatureReleaseService>()
        .AddScoped<IToPayRepository, ToPayRepository>()
        .AddScoped<IService<ToPayRequestDTO, ToPayResponseDTO, Guid>, ToPayService>()
        .AddScoped<IToReceiveRepository, ToReceiveRepository>()
        .AddScoped<IService<ToReceiveRequestDTO, ToReceiveResponseDTO, Guid>, ToReceiveService>();
}

static void ServiceConfiguration(WebApplicationBuilder builder)
{

    builder.Services
    .AddCors()
    .AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    }).AddNewtonsoftJson();

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Beaerer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
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
                Array.Empty<string>()
            }
        });

        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControleFacil.Api", Version = "v1" });   
    });

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })

    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["KeySecret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

static void AppConfiguration(WebApplication app)
{
    // Configura o contexto do postgreSql para usar timestamp sem time zone.
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.UseDeveloperExceptionPage()
        .UseRouting();

    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleFacil.Api v1");
                c.RoutePrefix = string.Empty;
        });

    app.UseCors(x => x
        .AllowAnyOrigin() // Permite todas as origens
        .AllowAnyMethod() // Permite todos os métodos
        .AllowAnyHeader()) // Permite todos os cabeçalhos
        .UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints => endpoints.MapControllers());

    app.MapControllers();
}