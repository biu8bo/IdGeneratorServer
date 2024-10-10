using IdGeneratorServer.Snowflake;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;
using Yitter.IdGenerator;

namespace IdGeneratorServer.YitterId;

public class YitterIdModule:AbpModule
{
 public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
 {
  var service = context.Services;
  var configuration = service.GetConfiguration();
  service.Configure<RedisOption>(configuration.GetSection("SnowflakeIdOption"));
  service.AddSingleton<ISnowflakeIdGenerator, YitterIdGenerator>();
  service.AddSingleton<IRedisClient, RedisClient>();
  service.AddSingleton<IDistributedGeneratorSupport, DistributedSupportWithRedis>();
  service.AddHostedService<SnowflakeBackgroundServices>();
  return base.ConfigureServicesAsync(context);
 }

 public override void OnApplicationInitialization(ApplicationInitializationContext context)
 {
  base.OnApplicationInitialization(context);
 }
}