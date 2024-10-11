using Volo.Abp;
using Volo.Abp.Application.Services;

namespace IdGeneratorServer.Application.Constant;

/// <summary>
/// Id生成服务
/// </summary>
public interface IdGeneratorAppService: IApplicationService
{
    /// <summary>
    /// 生成Id
    /// </summary>
    /// <returns></returns>
    Task<long> GetNextId();
}