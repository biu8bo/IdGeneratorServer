using IdGeneratorServer.Snowflake;

namespace IdGeneratorServer.YitterId;

public class RedisOption: SnowflakeOption
{
    public int Database { get; set; }
    public string ConnectionString { get; set; }
    public string InstanceName { get; set; }
}