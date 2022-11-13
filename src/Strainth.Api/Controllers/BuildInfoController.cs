using Microsoft.AspNetCore.Authorization;
using Strainth.Api.Build;

namespace Strainth.Api.Controllers;

public class BuildInfoController : StrainthApiBaseController
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<BuildInfo> GetBuildInfo()
    {
        return Ok(AppVersionInfo.GetBuildInfo());
    }
}