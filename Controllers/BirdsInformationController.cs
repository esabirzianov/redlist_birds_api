using System.Data;
using Microsoft.AspNetCore.Mvc;
using redlist_birds_api.Interfaces;
using redlist_birds_api.MethodsForRequests;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.Models;

namespace redlist_birds_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BirdsInformationController : ControllerBase
{   
    private readonly IMethodsForRequests _IMethodsForRequests;
    public IConfiguration _configuration ;
    public BirdsInformationController (IMethodsForRequests methodsForRequests, IConfiguration configuration) 
    {
        _IMethodsForRequests = methodsForRequests;
        _configuration = configuration;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult<List<RecentObservations>>> GetObservationsData()
    {
        var result = await _IMethodsForRequests.GetRecentObservations();
        return Ok(result);
    }

    [HttpGet("{comName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<List<RecentObservations>>> GetObservationsbyCommonName (string comName) 
    {
        var result =  await _IMethodsForRequests.GetRecentObservationsByCommonName(comName);
        return Ok(result);
    }

    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<RecentObservations>> PostNewObservation (string _comName, string _locName, int _howMany) 
    {
        var result =  await _IMethodsForRequests.CreateObservation(_comName, _locName, _howMany);
        return Ok(result);
    }

    [HttpDelete("{subId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteObservations (string subId) 
    {
        await _IMethodsForRequests.DeleteObservation(subId);
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<ActionResult> UpdateObservation (string subId, string comName, int howMany) 
    {
        await _IMethodsForRequests.UpdateObservation(subId,comName,howMany);
        return Ok();
    }
    
}

