using Application.Jobs;
using Core.Data.EF;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Presentation.API.Helpers;
using Presentation.API.Helpers.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

#region Services Configuration

builder.Services.AddDbContext<DataContext>(options => options
    .UseSqlServer(configuration.GetConnectionString("Sql")));

builder.Services.AddMvc();
builder.Services.ConfigureAuth(configuration);
builder.Services.ConfigureCors();
builder.Services.VersioningConfig();
builder.Host.AutofacConfiguration(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.FluentValidationConfiguration();
builder.Services.SwaggerConfiguration();
if (Convert.ToBoolean(configuration["Hangfire:Enabled"]))
    builder.Services.HangfireConfiguration(configuration);

#endregion

#region App Build
var app = builder.Build();

if (Convert.ToBoolean(configuration["EnableSwaggerUi"]))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Constants.CorsPolicyName);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();

    if (Convert.ToBoolean(configuration["Hangfire:Enabled"]))
    {
        app.UseHangfireDashboard("/jobs", new DashboardOptions()
        {
            DashboardTitle = "Hangfire Dashboard"
        });

        var executor = serviceScope.ServiceProvider.GetService<RecurrentJobsExecutor>();
        executor.ConfigureTasks();
    }
}

app.Run();
#endregion
