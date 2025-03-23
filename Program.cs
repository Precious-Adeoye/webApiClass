
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using webApiClass.Data;
using webApiClass.DTO;
using webApiClass.Iservice;
using webApiClass.model;
using webApiClass.Services;

namespace webApiClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>

            {
                opt.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Description = "Please follow this format. Bearer space token in double literal",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                opt.OperationFilter<SecurityRequirementsOperationFilter>();

                opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                 {
                {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
                }
             });
            });
            //Configuration of Automapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //database configuration

            string connectionString = builder.Configuration.GetConnectionString("connection");
            builder.Services.AddDbContext<StudentDBcontext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            //Configure Services
            _ = builder.Services.AddScoped<IStudent, StudentService>();
            builder.Services.AddScoped<IAuth, Authservices>();
            builder.Services.AddTransient<INumberCheckService, NumberCheckService>();

            // Identity Settings
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<StudentDBcontext>().AddSignInManager().AddRoles<IdentityRole>();

            //JWT settings
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
                };
            });


            builder.Services.AddHttpClient("Number-check", options =>
            {
                options.BaseAddress = new Uri("https://api.apilayer.com/");
                options.DefaultRequestHeaders.Add("apiKey", builder.Configuration["ApiLayerKey"]);
            });
            var app = builder.Build();

           
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
