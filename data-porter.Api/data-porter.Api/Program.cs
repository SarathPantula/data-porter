using core.Extensions.StartUpExtensions;
using data_porter.Api.Extensions;
using data_porter.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
});

builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostName, configuration) =>
{
    builder.Services.RegisterSerilogLogging(builder.Configuration, hostName, configuration);
});

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.ConfigureBaseExtensions(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<DbContext, DataPorterContext>().AddDbContext<DataPorterContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

builder.Services.RegisterAPIServices();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Data Porter v1");
});

app.UseSerilogRequestLogging();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.WithOrigins("*")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();