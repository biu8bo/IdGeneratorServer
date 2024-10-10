using System.Threading.Tasks;

namespace IdGeneratorServer.Snowflake;

/// <summary>
/// 分布式ID生成支持接口
/// </summary>
public interface IDistributedGeneratorSupport
{
    /// <summary>
    /// 获取下一个可用的机器id
    /// </summary>
    /// <returns></returns>
    Task<ushort> GetNextWorkId();
    /// <summary>
    /// 刷新机器id的存活状态
    /// </summary>
    /// <returns></returns>
    Task RefreshAlive();
}