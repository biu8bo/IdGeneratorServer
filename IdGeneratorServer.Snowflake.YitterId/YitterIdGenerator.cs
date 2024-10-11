using IdGeneratorServer.Snowflake;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Yitter.IdGenerator;

namespace IdGeneratorServer.YitterId;

/// <summary>
/// 分布式雪花ID实现
/// </summary>
public class YitterIdGenerator:ISnowflakeIdGenerator,ISingletonDependency
{
    private ushort _workId;
    private readonly SnowflakeOption _snowflakeOption;
    private readonly ILogger<YitterIdGenerator> _logger;
    public YitterIdGenerator(IServiceProvider provider,IOptions<SnowflakeOption> options,ILogger<YitterIdGenerator> logger)
    {
        try
        {
            _logger = logger;
            _snowflakeOption = options.Value;
            IDistributedGeneratorSupport distributed =      provider.GetService(typeof(IDistributedGeneratorSupport)) as IDistributedGeneratorSupport;
            if (distributed != null||!options.Value.WorkId.HasValue)
            {
                //获取唯一实例workid
                var t = distributed.GetNextWorkId();
                t.Wait();
                _workId = t.Result;
            }
            else
            {
                _workId = _snowflakeOption.WorkId.Value;
            }
            _logger.LogInformation("初始化创建实例WorkID:"+_workId);
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions(_workId));
        }
        catch (Exception e)
        {
            _logger.LogError("WorkId初始化失败");
            _logger.LogException(e);
            Environment.Exit(1);
        }
    }
    public long NextId()
    {
        return YitIdHelper.NextId();
    }
}