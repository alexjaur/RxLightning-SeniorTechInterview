using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions;
using Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

//
// basic configs
//

var patientsDataProviderSettings = new PatientsDataProviderSettings();
builder.Configuration.Bind("PatientsDataProviderSettings", patientsDataProviderSettings);
builder.Services.AddSingleton(patientsDataProviderSettings);

var jwtSettings = new JwtSettings();
builder.Configuration.Bind("JwtSettings", jwtSettings);
builder.Services.AddSingleton(jwtSettings);

//
// register internals
//

builder.Services.AddHttpClient(Constants.PatientsHttpClientName, httpClient =>
{
    httpClient.BaseAddress = new Uri(patientsDataProviderSettings.Host);
});

builder.Services.AddJwt(jwtSettings);

builder.Services.AddInternals();

//
// register commons
//

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});


//
// build app & configure the HTTP request pipeline.
//

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(x =>
        x.AllowAnyHeader()
         .AllowAnyMethod()
         .WithOrigins("http://localhost:3000")
    );
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
