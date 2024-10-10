namespace IdGeneratorServer.Snowflake;

/// <summary>
/// 雪花ID生成
/// </summary>
public interface ISnowflakeIdGenerator
{
    /// <summary>
    /// 获取id
    /// </summary>
    /// <param name="workId"></param>
    /// <returns></returns>
    long NextId();
}