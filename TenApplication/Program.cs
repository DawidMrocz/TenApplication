using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Models;
using TenApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        providerOptions => { providerOptions.EnableRetryOnFailure(); });
    options.LogTo(Console.WriteLine);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});


// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();

// builder.Services.Configure<IdentityOptions>(options =>
// {
//     // Password settings.
//     options.Password.RequireDigit = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = true;
//     options.Password.RequireUppercase = true;
//     options.Password.RequiredLength = 6;
//     options.Password.RequiredUniqueChars = 1;

//     // Lockout settings.
//     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//     options.Lockout.MaxFailedAccessAttempts = 5;
//     options.Lockout.AllowedForNewUsers = true;

//     // User settings.
//     options.User.AllowedUserNameCharacters =
//     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//     options.User.RequireUniqueEmail = false;
// });

// builder.Services.ConfigureApplicationCookie(options =>
// {
//     // Cookie settings
//     options.Cookie.HttpOnly = true;
//     options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

//     options.LoginPath = "/Identity/Account/Login";
//     options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//     options.SlidingExpiration = true;
// });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "SampleInstance";
});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(option =>
//    {
//        option.LoginPath = "/Access/Login";
//        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher<Designer>, PasswordHasher<Designer>>();

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<ICatRepository, CatRepository>();
builder.Services.AddScoped<IRaportRepository, RaportRepository>();
builder.Services.AddScoped<IDesignerRepository, DesignerRepository>();
builder.Services.AddScoped<IInboxRepository, InboxRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
