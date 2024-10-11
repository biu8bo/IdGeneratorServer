using IdGeneratorServer.Application.Constant;
using IdGeneratorServer.YitterId;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace IdGeneratorServer.Application;

[DependsOn(typeof(YitterIdModule),
    typeof(AbpDddApplicationModule),
    typeof(ApplicationConstantModule))]
public class ApplicationModule:AbpModule{}