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
    //p�id�n� rol�
    .AddRoles<IdentityRole>() // P�id�n� podpory pro role
    .AddEntityFrameworkStores<ApplicationDbContext>() // Pou�it� Entity Framework pro ukl�d�n� u�ivatel� a rol�
    .AddSignInManager() // P�id�n� spr�vce p�ihl�en�
    .AddDefaultTokenProviders() // P�id�n� v�choz�ch poskytovatel� token�
    .AddApiEndpoints(); // mo�n� P�id�n� podpory pro API Endpoints


    
    var app = builder.Build();

    // Konfigurace middleware pro Identity
    app.MapIdentityApi<ApplicationUser>(); // P�id�n� mapov�n� API pro Identity


//p��dan� role a defaultn�ho u�ivatele
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

    // Vytvo�en� defaultn�ho admin u�ivatele
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
        // P�id�n� vlastn�ch vlastnost� u�ivatele, pokud je pot�eba
        public string FullName { get; set; }
    }