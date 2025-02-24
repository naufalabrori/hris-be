using HRIS.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HRIS.Infrastructure.Utils;
using HRIS.Infrastructure.Utils.Interfaces;
using HRIS.Infrastructure.Extensions.Logging;
using Microsoft.OpenApi.Models;
using HRIS.Core.Interfaces.Repositories;
using HRIS.Data.Repositories;
using HRIS.Core.Interfaces.Services;
using HRIS.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HRIS.Core.Dto;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Konfigurasi Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Log hanya level Information ke atas
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Abaikan log info dari Microsoft
    //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) // Abaikan detail dari ASP.NET Core
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning) // Menyembunyikan log query database
    .WriteTo.Console()
    .WriteTo.File("Logs/hris-log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog(); // Menggunakan Serilog sebagai logging provider

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
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IJobTitleRepository, JobTitleRepository>();
builder.Services.AddScoped<IJobTitleService, JobTitleService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IEmployeeTrainingRepository, EmployeeTrainingRepository>();
builder.Services.AddScoped<IEmployeeTrainingService, EmployeeTrainingService>();
builder.Services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
builder.Services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();
builder.Services.AddScoped<IBenefitRepository, BenefitRepository>();
builder.Services.AddScoped<IBenefitService, BenefitService>();
builder.Services.AddScoped<IRecruitmentRepository, RecruitmentRepository>();
builder.Services.AddScoped<IRecruitmentService, RecruitmentService>();
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddDbContext<HrisContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            builder.Configuration["AppSettings:Secret"]))
    };

    // Configure custom response for unauthorized access
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse(); // Prevent default response
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var response = new ApiResponseDto<object>
            {
                Success = false,
                Message = "Unauthorized. Token is missing or invalid."
            };

            await context.Response.WriteAsJsonAsync(response);
        },
        OnForbidden = async context =>
        {
            Console.WriteLine("OnForbidden");
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";

            var response = new ApiResponseDto<object>
            {
                Success = false,
                Message = "Forbidden. You do not have access to this resource."
            };

            await context.Response.WriteAsJsonAsync(response);
        },

    };
});

var app = builder.Build();
app.MapGet("/ping", () => "PONG!");

// Configure the HTTP request pipeline.
Log.Information("Start configuring http request pipeline");

// Ensure the tables are created
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetService<HrisContext>();
    context?.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Tambahkan middleware logging

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
