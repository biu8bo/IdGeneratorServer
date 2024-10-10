using IdGeneratorServer.Application.Constant;
using IdGeneratorServer.Snowflake;
using Volo.Abp.Application.Services;
using Yitter.IdGenerator;

namespace IdGeneratorServer.Application;

public class YitIdGeneratorService: ApplicationService,IdGeneratorService
{
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    public YitIdGeneratorService(ISnowflakeIdGenerator snowflakeIdGenerator)
    {
        _snowflakeIdGenerator = snowflakeIdGenerator;
    }
    public long NextId()
    {
 
        return  _snowflakeIdGenerator.NextId();
    }
}