using FacilityFlow.Application.Abstractions;
using FacilityFlow.Infrastructure.Persistence;
using FacilityFlow.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FacilityFlow.Infrastructure;
public static class DependencyInjection
{
 public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
 { var cs=configuration.GetConnectionString("DefaultConnection")??"Data Source=facilityflow.db";services.AddDbContext<FacilityFlowDbContext>(o=>o.UseSqlite(cs));services.AddScoped<IServiceRequestService,ServiceRequestService>();return services; }
}
