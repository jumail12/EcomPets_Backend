using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Pets_Project_Backend.Context;
using Pets_Project_Backend.Services.Auth_Services;
using Pets_Project_Backend.Mapper;
using Pets_Project_Backend.Services.Category_Services;
using Pets_Project_Backend.Services.Product_Services;
using Pets_Project_Backend.CloudinaryServices;
using Microsoft.AspNetCore.Http.Features;
using Pets_Project_Backend.Services.CartServices;
using Pets_Project_Backend.CustomMiddilWare;
using Pets_Project_Backend.Services.WhishList_Service;
using Pets_Project_Backend.Services.Order_Services;
using Pets_Project_Backend.Services.AddressServices;
using Pets_Project_Backend.Services.UserServices;

namespace Pets_Project_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            //----------------------------------------------------------------------------------------

           
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PetsEcom  API", Version = "v1" });

                // Add JWT Authentication in Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            //----------------------------------------------------------------------------------------


            //// Context service and connection string setup
            //builder.Services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,                     // Retry up to 5 times
            maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
            errorNumbersToAdd: null                 // Optional: Additional SQL error codes to treat as transient
        )
    )
);


            builder.Services.AddAutoMapper(typeof(MapperProfile));


            //----------------------------------------------------------------------------------------
            // Service registration
            builder.Services.AddScoped<IAuthServices, Auth_Services>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IWhishListService, WhishListService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IUserService, UserService>();
            //cloudinary
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            //----------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------

            // JWT service
            // Jwt authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization();

            //----------------------------------------------------------------------------------------
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            //----------------------------------------------------------------------------------------



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("ReactPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Must be before UseAuthorization
            app.UseAuthorization();

            app.UseMiddleware<UserIdMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}