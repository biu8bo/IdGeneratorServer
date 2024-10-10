using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace IdGeneratorServer.Snowflake;
public class SnowflakeBackgroundServices : BackgroundService
{
    private readonly ISnowflakeIdGenerator _idMaker;
    private readonly IDistributedGeneratorSupport _distributed;
    private readonly SnowflakeOption option;
    public SnowflakeBackgroundServices(ISnowflakeIdGenerator idMaker, IDistributedGeneratorSupport distributed, IOptions<SnowflakeOption> options)
    {
        _idMaker = idMaker;
        option = options.Value;
        _distributed = distributed;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
            while (!stoppingToken.IsCancellationRequested)
            {
                //定时刷新机器id的存活状态
                await _distributed.RefreshAlive();
                await Task.Delay(option.RefreshAliveInterval.Add(TimeSpan.FromMinutes(1)), stoppingToken);
            }
    }
}