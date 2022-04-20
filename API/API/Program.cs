using API.Data;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(x =>
{
   x.AddPolicy("CorsPolicy", policy =>
   {
       policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
   });
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddDbContext<DataContext>(options =>

                options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"))
            );
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("RequireRefugeeRole", policy => policy.RequireRole("Refugee"));
    opt.AddPolicy("RequireBenefactorRole", policy => policy.RequireRole("Benefactor"));
    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.SignIn.RequireConfirmedAccount = false;
})
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddSignInManager<SignInManager<AppUser>>()
               .AddRoleValidator<RoleValidator<AppRole>>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<DataContext>();

//builder.Services.AddTransient<IEmailSender, MailJetEmailSender>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY"))),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                   };
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

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
