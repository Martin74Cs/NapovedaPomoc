using System.Text;

namespace NapovedaPomoc {
    public class AddAuthentication {
        
        // Přidání autentizace pomocí JWT
        var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(option =>
        {
                option.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    //ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                    ValidIssuer = appSettings.Issuer,
                    ValidateAudience = true,
                    //ValidAudience = builder.Configuration["AppSettings:Audience"],
                    ValidAudience = appSettings.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        //Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
                        Encoding.UTF8.GetBytes(appSettings.Token)),
                    ValidateIssuerSigningKey = true,

                };

            });

        // Přidání autentizace pomocí cookies a Identity
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/authentication/login";
            options.AccessDeniedPath = "/authentication/access-denied";
        })
        .AddIdentityCookies();

        // Přidání autentizace pomocí cookies a Identity (zkráceně)
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        // potom můžeme asi použít
        // Konfigurace cookies pro Identity
        builder.Services.ConfigureApplicationCookie(options => { });

        //Nerozumím proč tohle je potřeba
        // Přidání autorizace s politikami a rolemi pro Identity 
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("EditPolicy", policy =>
                policy.RequireRole("Edit"));
        });


    }
}
