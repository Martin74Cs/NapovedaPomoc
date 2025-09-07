namespace NapovedaPomoc {
    public class Cords {

        // Přidání CORS služby
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                policy =>
                {
                    policy.WithOrigins("https://localhost:7178") // povolit tuto adresu
                          .AllowAnyHeader()                    // povolit všechny hlavičky
                          .AllowAnyMethod();                   // povolit všechny HTTP metody
                });
        });

        // Použití CORS
        app.UseCors("AllowLocalhost");
    }
}
