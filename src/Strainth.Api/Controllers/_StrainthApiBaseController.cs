using Microsoft.AspNetCore.Mvc;

namespace Strainth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StrainthApiBaseController : ControllerBase
{
    protected BadRequestResult HandleBadRequest<Tlog>(ILogger<Tlog> logger, object[] badRequestParams) where Tlog : StrainthApiBaseController
    {
        logger.LogWarning("Bad request due to following params: {@Params}", badRequestParams);
        return BadRequest();
    }

    protected NotFoundResult HandleNotFoundRequest<Tlog>(ILogger<Tlog> logger, string dtoType, object[] notFoundParams) where Tlog : StrainthApiBaseController
    {
        logger.LogWarning("DTO of type {dtoType} not found using following params: {@Params}", dtoType, notFoundParams);
        return NotFound();
    }
}