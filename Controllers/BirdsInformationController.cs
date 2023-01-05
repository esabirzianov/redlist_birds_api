using System.Data;
using Microsoft.AspNetCore.Mvc;
using redlist_birds_api.MethodsForController;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.Models;

namespace redlist_birds_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BirdsInformationController : ControllerBase
{   
    private readonly IBirdInformationRequests _birdInformationRequests;
    public BirdsInformationController (IBirdInformationRequests birdInformationRequests) 
    {
        _birdInformationRequests = birdInformationRequests;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult<List<RecentObservations>>> GetObservationsData()
    {
        var result = await _birdInformationRequests.GetRecentObservations();
        return Ok(result);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{comName}")]

    public async Task<ActionResult<List<RecentObservations>>> GetObservationsbyCommonName (string comName) 
    {
        var result =  await _birdInformationRequests.GetRecentObservationsByCommonName(comName);
        if (result.Count == 0) 
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<RecentObservations>> PostNewObservation (string _comName, string _locName, int _howMany) 
    {
        var result =  await _birdInformationRequests.CreateObservation(_comName, _locName, _howMany);
        return Created("api/[controller]", result);
    }

    [HttpDelete("{subId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<ActionResult> DeleteObservations (string subId) 
    {
        var result = await _birdInformationRequests.GetRecentObservationsBySubId(subId);
        if (result.Count == 0) 
        {
            return NotFound();
        }
        await _birdInformationRequests.DeleteObservation(subId);
        return NoContent();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<ActionResult> UpdateObservation (string subId, string comName, int howMany) 
    {
        var result = await _birdInformationRequests.GetRecentObservationsBySubId(subId);
        if (result.Count == 0) 
        {
            return NotFound();
        }
        await _birdInformationRequests.UpdateObservation(subId,comName,howMany);
        return NoContent();
    }
    
}

