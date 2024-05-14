using System.Net.Http;

namespace csharp_gregslist_api.Controllers;

[ApiController]
[Route("api/houses")]
public class HousesConroller : ControllerBase
{
    private readonly HousesService _housesService;
    private readonly Auth0Provider _auth0provider;

    public HousesConroller(HousesService housesService, Auth0Provider auth0provider)
    {
        _housesService = housesService;
        _auth0provider = auth0provider;
    }

    [HttpGet]
    public ActionResult<List<House>> GetHouses()
    {
        try
        {
            List<House> houses = _housesService.GetHouses();
            return Ok(houses);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{houseId}")]
    public ActionResult<House> GetHouseById(int houseId)
    {
        try
        {
            House house = _housesService.GetHouseById(houseId);
            return Ok(house);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<House>> CreateHouse([FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);

            houseData.CreatorId = userInfo.Id;

            House house = _housesService.CreateHouse(houseData);
            return Ok(house);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [Authorize]
    [HttpPut("{houseId}")]
    public async Task<ActionResult<House>> UpdateHouse(int houseId, [FromBody] House houseData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);

            House house = _housesService.UpdateHouse(houseId, userInfo.Id, houseData);
            return Ok(house);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [Authorize]
    [HttpDelete("{houseId}")]
    public async Task<ActionResult<House>> DestroyHouse(int houseId)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);

            string message = _housesService.DestroyHouse(houseId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}