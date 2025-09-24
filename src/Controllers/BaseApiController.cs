using Microsoft.AspNetCore.Mvc;

namespace PerlaMetroApiMain.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class BaseApiController : ControllerBase { }