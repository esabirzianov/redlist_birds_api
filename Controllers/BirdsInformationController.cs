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
    private readonly IBirdInformationRequests _birdInformationRequests;
    public IConfiguration _configuration ;

    public BirdsInformationController (IBirdInformationRequests birdInformationRequests, IConfiguration configuration) 
    {
        _birdInformationRequests = birdInformationRequests;
        _configuration = configuration;
    }
    
    [HttpGet]
    public async Task<List<RecentObservations>> GetObservationsData()
    {
        return await _birdInformationRequests.GetRecentObservations();
    }

    [HttpGet("{comName}")]

    public async Task<List<RecentObservations>> GetObservationsbyCommonName (string comName) 
    {
       
        return await _birdInformationRequests.GetRecentObservationsByCommonName(comName);
    }

    
    [HttpPost]
    public async Task<RecentObservations> PostNewObservation (string _comName, string _locName, int _howMany) 
    {
        return await _birdInformationRequests.CreateNewObservation(_comName, _locName, _howMany);
    }

    [HttpDelete("{sunId}")]

    public async Task DeleteObservations (string subId) 
    {
        await _birdInformationRequests.DeleteObservation(subId);
    }
    
    [HttpPut]
    
    public async Task<RecentObservations> UpdateObservation (string subId, string comName, int howMany) 
    {
       return await _birdInformationRequests.UpdateObservation(subId,comName,howMany);
    }

    
}

