using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Web.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDbContext") ?? throw new InvalidOperationException("Connection string 'SchoolDbContext' not found.")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();
app.Logger.LogInformation("Starting up...");
app.Logger.LogInformation("Environment: {Environment}", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<SchoolDbContext>();

    if (app.Environment.IsDevelopment() && (await dbContext.Database.GetPendingMigrationsAsync()).Any())
    {
        app.Logger.LogInformation("Running database migrations...");
        await dbContext.Database.MigrateAsync();
        app.Logger.LogInformation("Database migrations complete");
    }

    app.Logger.LogInformation("Initialising database...");
    SchoolDbInitialiser.Initialise(dbContext);
    app.Logger.LogInformation("Database initialised");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
