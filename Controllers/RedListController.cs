using Microsoft.AspNetCore.Mvc;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.MethodsForController;
using redlist_birds_api.Models;

namespace redlist_birds_api.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class RedListController : ControllerBase
{
    private readonly IRedListRequests _redListRequests;
    public RedListController(IRedListRequests redListRequests)
    {
        _redListRequests = redListRequests;
    }

    [HttpGet("/api/[controller]/redlistdatabase")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RedListData>>> GetRedListData()
    {
        var result = await _redListRequests.GetRedListDataAsync();
        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<RedListRecentObservations>>> GetRedListRecentObservationsAsync () 
    {
        var result = await _redListRequests.GetRedListRecentObservationsAsync();
        return Ok(result);
    }

}