using HRIS.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HRIS.Infrastructure.Utils;
using HRIS.Infrastructure.Utils.Interfaces;
using Microsoft.OpenApi.Models;
using HRIS.Core.Interfaces.Repositories;
using HRIS.Data.Repositories;
using HRIS.Core.Interfaces.Services;
using HRIS.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HRIS API",
        Description = "HRIS Documentation API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    //options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SupportNonNullableReferenceTypes();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
});

builder.Services.AddSingleton<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();

builder.Services.AddScoped<IHrisRepository, HrisRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddDbContext<HrisContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.MapGet("/ping", () => "PONG!");

// Ensure the tables are created
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetService<HrisContext>();
    context?.Database.Migrate();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
//app.UseCors();
//app.UseCors("WMSCors");
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true)); // allow any origin
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "HRIS v1"));
}

try
{
    app.Run();
    return 0;
}
catch (Exception ex)
{
    return 1;
}
finally
{
    //connection.Close();
    Thread.Sleep(2000);
}
