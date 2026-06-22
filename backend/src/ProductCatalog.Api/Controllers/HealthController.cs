using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public HealthController(ISqlSugarClient db)
    {
        _db = db;
    }

    [HttpGet("db")]
    public async Task<IActionResult> Database()
    {
        try
        {
            var result = await _db.Ado.GetIntAsync("SELECT 1");

            return Ok(new
            {
                status = "Ok",
                database = "Connected",
                result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "ERROR",
                message = ex.Message
            });
        }
    }
}