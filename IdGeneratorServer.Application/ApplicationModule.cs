using IdGeneratorServer.YitterId;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace IdGeneratorServer.Application;

[DependsOn(typeof(YitterIdModule))]
public class ApplicationModule:AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);
    }
}