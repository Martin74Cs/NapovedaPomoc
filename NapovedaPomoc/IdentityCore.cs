using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Identity;
using NapovedaPomoc;

var builder = WebApplication.CreateBuilder(args);

//Microsoft.EntityFrameworkCore.InMemory
//<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.8" 
//< PackageReference Include = "Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore" Version = "8.0.7" />
//< PackageReference Include = "Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version = "8.0.7" />
//< PackageReference Include = "Microsoft.EntityFrameworkCore.SqlServer" Version = "8.0.7" />
//< PackageReference Include = "Microsoft.EntityFrameworkCore.Tools" Version = "8.0.7" />
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString));
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;

        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<ApplicationUser>( options => {
    options.SignIn.RequireConfirmedAccount = true; 
    options.Password.RequiredLength = 2;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    })
    //pøidání rolí
    .AddRoles<IdentityRole>() // Pøidání podpory pro role
    .AddEntityFrameworkStores<ApplicationDbContext>() // Použití Entity Framework pro ukládání uživatelù a rolí
    .AddSignInManager() // Pøidání správce pøihlášení
    .AddDefaultTokenProviders() // Pøidání výchozích poskytovatelù tokenù
    .AddApiEndpoints(); // možná Pøidání podpory pro API Endpoints


    
    var app = builder.Build();

    // Konfigurace middleware pro Identity
    app.MapIdentityApi<ApplicationUser>(); // Pøidání mapování API pro Identity


//pøídaní role a defaultního uživatele
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Vytvoøení defaultního admin uživatele
    //var adminUser = new IdentityUser
    var adminUser = new ApplicationUser
    {
        UserName = "admin@example.com",
        Email = "admin@example.com",
        EmailConfirmed = true
    };

    var user = await userManager.FindByEmailAsync(adminUser.Email);
    if (user == null)
    {
        var createAdmin = await userManager.CreateAsync(adminUser, "admin@example.com");
        if (createAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    
}

    public class ApplicationUser : IdentityUser
    {
        // Pøidání vlastních vlastností uživatele, pokud je potøeba
        public string FullName { get; set; }
    }