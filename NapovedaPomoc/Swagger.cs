//Balíčky Swagger
<PackageReference Include = "Swashbuckle.AspNetCore.SwaggerGen" Version = "6.5.0" />
<PackageReference Include = "Swashbuckle.AspNetCore.SwaggerUI" Version = "6.5.0" />

//přidání ověřování do swaggeru
Soubor Program.cs

    builder.Services.AddSwaggerGen(option =>
    {
        option.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "Description",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Name = "x-api-key",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Scheme = "ApiKeyScheme"
        });
        var Scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "ApiKey"
            },
            In = ParameterLocation.Header,
        };
        var requrement = new OpenApiSecurityRequirement
                    {
                        { Scheme, new List<string>() }
                    };
        option.AddSecurityRequirement(requrement);
    });

app.UseSwagger();
app.UseSwaggerUI();


//pouziti swagger z Http
//https://localhost:7105/Swagger/index.html