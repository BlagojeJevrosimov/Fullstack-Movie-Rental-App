using Azure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using MovieStore.Api.Infrastructure;
using MovieStore.Api.Services;
using MovieStore.Core.Entities;
using MovieStore.Infrastructure;
using MovieStore.Infrastructure.Contracts;
using MovieStore.Infrastructure.Repositories;
using Serilog;



try
{
    var builder = WebApplication.CreateBuilder(args);

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.Configure<BackgroundServiceOptions>(builder.Configuration.GetSection(BackgroundServiceOptions.SectionName));

    builder.Services.AddHostedService<ExpirationCheckService>();

    builder.Services.AddTransient<IEmailService, GmailEmailService>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin();
                              policy.AllowAnyHeader().AllowAnyMethod();
                          });
    });
    builder.Services.AddControllers();

    builder.Services.AddDbContext<MovieStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

    builder.Services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
    builder.Services.AddScoped(typeof(IRepository<Movie>), typeof(MovieRepository));
    builder.Services.AddScoped(typeof(IRepository<PurchasedMovie>), typeof(PurchasedMovieRepository));

    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    builder.Services.AddOpenApiDocument(cfg => cfg.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator());

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");


    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });

    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
    });

    var app = builder.Build();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseOpenApi();
    app.UseSwaggerUi3();

    app.MapControllers();

    app.UseCors(MyAllowSpecificOrigins);

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider
        .GetRequiredService<MovieStoreContext>();
    dbContext.Database.Migrate();


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
