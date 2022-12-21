using System.Data;
using Microsoft.AspNetCore.Mvc;
using redlist_birds_api.Interfaces;
using redlist_birds_api.MethodsForRequests;
using redlist_birds_api.DatabaseConnection;
using redlist_birds_api.Models;

namespace redlist_birds_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BirdsInformationController : ControllerBase
{   
    private readonly IGetMethods _GetMethods;
   

    public BirdsInformationController (IGetMethods getMethod) 
    {
        _GetMethods = getMethod;
    }
    
    [HttpGet]
    public async Task<List<RecentObservations>> GetObservationsData()
    {
        List<RecentObservations> recents = new List<RecentObservations> ();
        recents = await _GetMethods.GetRecentObservations();
        foreach (RecentObservations value in recents) 
        {
            DatabaseSetup.OpenConnection (DatabaseSetup.connectionString, value.comName, value.howMany);
        }
        
        return recents;
    }

    [HttpGet("{comName}")]

    public async Task<List<RecentObservations>> GetObservationsbyId (string comName) 
    {
        List<RecentObservations> commonNameObs = new List<RecentObservations> ();
        commonNameObs = await _GetMethods.GetRecentObservationsByCommonName(comName);
        return commonNameObs;
    }

    [HttpPost]
    public async Task<List<RecentObservations>> PostNewObservation (string _comName, string _locName, int _howMany) 
    {
        List<RecentObservations> postnewObservation = new List<RecentObservations> ();
        postnewObservation = await _GetMethods.CreateNewObservation(_comName, _locName, _howMany);
        return postnewObservation;
    }

    [HttpPut]
    public async Task<RecentObservations> UpdateObservations (string obsId, string comName, int howMany) 
    {
        RecentObservations updatednewObservation = new RecentObservations ();
        updatednewObservation = await _GetMethods.UpdateObservation(obsId, comName, howMany);
        return updatednewObservation;
    }


    
}

