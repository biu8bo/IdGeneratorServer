using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace IdGeneratorServer.Application.Constant;

[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class ApplicationConstantModule:AbpModule
{
    
}