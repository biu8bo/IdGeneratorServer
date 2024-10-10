﻿using Volo.Abp;
using Volo.Abp.Application.Services;

namespace IdGeneratorServer.Application.Constant;

/// <summary>
/// Id生成服务
/// </summary>
public interface IdGeneratorService: IApplicationService
{
    /// <summary>
    /// 生成Id
    /// </summary>
    /// <returns></returns>
     long NextId();
}