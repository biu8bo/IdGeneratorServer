using IdGeneratorServer.Application.Constant;
using Microsoft.AspNetCore.Mvc;

namespace IdGeneratorServer.Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class IdController:ControllerBase
{
    private readonly IdGeneratorService _idGeneratorService;

    public  IdController(IdGeneratorService idGeneratorService)
    {
        _idGeneratorService = idGeneratorService;
    }
    [HttpGet]
    public long Get()
    {
      return  _idGeneratorService.NextId();
    }
}