using IdGeneratorServer.Application.Constant;
using IdGeneratorServer.Snowflake;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Yitter.IdGenerator;

namespace IdGeneratorServer.Application;

 
public class YitIdGeneratorAppService: ApplicationService,IdGeneratorAppService
{
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    public YitIdGeneratorAppService(ISnowflakeIdGenerator snowflakeIdGenerator)
    {
        _snowflakeIdGenerator = snowflakeIdGenerator;
    }
    
    public async Task<long> GetNextId()
    {
        return await  Task.FromResult(_snowflakeIdGenerator.NextId());
    }
}