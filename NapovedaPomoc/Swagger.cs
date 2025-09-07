////Balíčky Swagger
//<PackageReference Include = "Swashbuckle.AspNetCore.SwaggerGen" Version = "6.5.0" />
//<PackageReference Include = "Swashbuckle.AspNetCore.SwaggerUI" Version = "6.5.0" />

//Swashbuckle.AspNetCore.Swagger
//Swashbuckle.AspNetCore.SwaggerGen
//Swashbuckle.AspNetCore.SwaggerUI
//<PackageReference Include="Swashbuckle.AspNetCore.Swagger" Version="6.8.1" />
//<PackageReference Include="Swashbuckle.AspNetCore.SwaggerGen" Version="6.8.1" />
//<PackageReference Include="Swashbuckle.AspNetCore.SwaggerUI" Version="6.8.1" />

////přidání ověřování do swaggeru
////valášk má video o přidávání oveřování ado swagger
//Soubor Program.cs

//    builder.Services.AddSwaggerGen(option =>
//    {
//        option.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//        {
//            Description = "Description",
//            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//            Name = "x-api-key",
//            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//            Scheme = "ApiKeyScheme"
//        });
//        var Scheme = new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference
//            {
//                Type = ReferenceType.SecurityScheme,
//                Id = "ApiKey"
//            },
//            In = ParameterLocation.Header,
//        };
//        var requrement = new OpenApiSecurityRequirement
//                    {
//                        { Scheme, new List<string>() }
//                    };
//        option.AddSecurityRequirement(requrement);
//    });

//app.UseSwagger();
//app.UseSwaggerUI();


////pouziti swagger z Http
////https://localhost:7105/Swagger/index.html

//Jiné rozhraní pro testování Swagger je OpenApi. 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


    app.MapOpenApi();
    //https://localhost:7084/openapi/v1.json
    app.MapScalarApiReference();
    //https://localhost:7084/scalar/v1
    app.UseSwaggerUI();
    app.UseSwagger();
    //https://localhost:7084/swagger/index.html