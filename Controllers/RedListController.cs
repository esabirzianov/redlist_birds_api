using Microsoft.AspNetCore.Mvc;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.Models;

namespace redlist_birds_api.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class RedListController : ControllerBase
{
    [HttpGet]
    public List<RedListData> GetRedListData () 
    {
        return RedListOperator.LoadRedListDataJson();
    }


}