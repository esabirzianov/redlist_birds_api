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
    private readonly IGetMethods _GetMethods;
    public IConfiguration _configuration ;

    public BirdsInformationController (IGetMethods getMethod, IConfiguration configuration) 
    {
        _GetMethods = getMethod;
        _configuration = configuration;
    }
    
    [HttpGet]
    public async Task<List<RecentObservations>> GetObservationsData()
    {
        return await _GetMethods.GetRecentObservations();
    }

    [HttpGet("{comName}")]

    public async Task<List<RecentObservations>> GetObservationsbyCommonName (string comName) 
    {
       
        return await _GetMethods.GetRecentObservationsByCommonName(comName);
    }

    
    [HttpPost]
    public async Task<RecentObservations> PostNewObservation (string _comName, string _locName, int _howMany) 
    {
        return await _GetMethods.CreateNewObservation(_comName, _locName, _howMany);
    }

    [HttpDelete("{sunId}")]

    public async Task DeleteObservations (string subId) 
    {
        await _GetMethods.DeleteObservation(subId);
    }
    
    [HttpPut]
    
    public async Task<RecentObservations> UpdateObservation (string subId, string comName, int howMany) 
    {
       return await _GetMethods.UpdateObservation(subId,comName,howMany);
    }

    


    
}

