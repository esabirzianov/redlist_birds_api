using System.Data;
using Microsoft.AspNetCore.Mvc;
using redlist_birds_api.MethodsForRequests;
using redlist_birds_api.Models;

namespace redlist_birds_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BirdsInformationController : ControllerBase
{   
    private readonly IGetMethods _GetMethods;
    public List<RecentObservations> stringMessage;

    public BirdsInformationController (IGetMethods getMethod) 
    {
        _GetMethods = getMethod;
    }
    
    [HttpGet]
    public async Task<List<RecentObservations>> LoadObservationsDataByRegion()
    {
        List<RecentObservations> holidays = new List<RecentObservations> ();
        holidays = await _GetMethods.GetRecentObservations();
        return holidays;
    }

    
    
    
}

