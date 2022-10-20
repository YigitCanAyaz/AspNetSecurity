using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    // for accepting all api request
    //options.AddDefaultPolicy(builder =>
    //{
    //    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //});

    //options.AddPolicy("AllowedSites", builder =>
    //{
    //    builder.WithOrigins("https://localhost:7233", "https://mysite.com").AllowAnyHeader().AllowAnyMethod();
    //});

    // header setting
    //options.AddPolicy("AllowedSites2", builder =>
    //{
    //    builder.WithOrigins("https://mysite2.com").WithHeaders(HeaderNames.ContentType, "x-custom-header");
    //});

    // accepts any sub domain
    options.AddPolicy("AllowedSites", builder =>
    {
        builder.WithOrigins("https://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();

        // builder.AllowCredential();
        // builder.DisableCredential();
    });

    options.AddPolicy("AllowedSites2", builder =>
    {
        builder.WithOrigins("https://localhost:7234").WithMethods("POST", "GET").AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// if it's controller/method level then not write anything into the parameter
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
