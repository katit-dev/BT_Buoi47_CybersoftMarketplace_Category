using System.Security.Claims;
using System.Text;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// ============================================================
// DI SWAGGER
// ============================================================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Viết doc cho Swagger API
    // Nạp file XML chứa chú thích (summary, response...)
    // để hiển thị trên Swagger UI
    var xmlFile =
        $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = System.IO.Path.Combine(
        AppContext.BaseDirectory,
        xmlFile
    );

    if (System.IO.File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "API documentation for .NET 10"
    });

    // Khai báo scheme Bearer
    // Tạo nút Authorize và ô nhập token trong Swagger
    options.AddSecurityDefinition(
        "bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Nhập token JWT vào ô dưới đây"
        }
    );
});

// ============================================================
// DI AUTHENTICATION - AUTHORIZATION BẰNG JWT
// ============================================================

var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

// Cấu hình Authentication sử dụng JWT Bearer
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                // Xác thực khóa bí mật của token
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key!)
                ),

                // Xác thực Issuer
                ValidateIssuer = true,

                // Phải khớp với Issuer trong token
                ValidIssuer = issuer,

                // Xác thực Audience
                ValidateAudience = true,

                // Phải khớp với Audience trong token
                ValidAudience = audience,

                // Xác thực thời gian hết hạn của token
                ValidateLifetime = true,

                // Không cho phép độ trễ sau khi token hết hạn
                ClockSkew = TimeSpan.Zero,

                // Ánh xạ claim chứa role
                RoleClaimType = ClaimTypes.Role,

                // Ánh xạ claim chứa tên người dùng
                NameClaimType = "UserName"
            };
    });

// Đăng ký dịch vụ phân quyền
builder.Services.AddAuthorization();

// DI DbContext
builder.Services.AddDbContext<CybersoftMarketPlaceContext>();

//DI ef 
//bật proxies

string connectionString = builder.Configuration.GetConnectionString("DBConnectionstring");
builder.Services.AddDbContext<CybersoftMarketPlaceContext>();



// DI Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomer1Repository, Customer1Repository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

//di jwt service
builder.Services.AddScoped<JwtAuthService>();

//DI UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


//DI Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
// builder.Services.AddScoped<ICartService, CartService>();
// builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Khai cors cho fe : http://localhost:5279
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:5279") // Thay đổi URL của FE nếu cần
               .AllowAnyHeader()
               .AllowCredentials()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

app.MapControllers();

app.UseCors("AllowSpecificOrigin");

// ============================================================
// SWAGGER MIDDLEWARE
// ============================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Chuyển HTTP sang HTTPS
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "My API V1"
    );

    // Đặt Swagger UI tại đường dẫn gốc:
    // http://localhost:<port>/
    options.RoutePrefix = string.Empty;
});


app.Run();