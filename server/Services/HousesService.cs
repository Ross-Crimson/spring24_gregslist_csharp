

namespace csharp_gregslist_api.Services;

public class HousesService
{
    private readonly HousesRepository _repository;


    public HousesService(HousesRepository repository)
    {
        _repository = repository;
    }

    internal House CreateHouse(House houseData)
    {
        House house = _repository.CreateHouse(houseData);
        return house;
    }

    internal string DestroyHouse(int houseId, string userId)
    {
        House houseToDestroy = GetHouseById(houseId);

        if (houseToDestroy.CreatorId != userId)
        {
            throw new Exception("You cannot destroy a house you don't own");
        }
        _repository.DestroyHouse(houseId);

        return $"{houseToDestroy.Id} has been deleted";
    }

    internal House GetHouseById(int houseId)
    {
        House house = _repository.GetHouseById(houseId);

        if (house == null)
        {
            throw new Exception($"Could not find house {houseId}");
        }

        return house;
    }

    internal List<House> GetHouses()
    {
        List<House> houses = _repository.GetHouses();
        return houses;
    }

    internal House UpdateHouse(int houseId, string userId, House houseData)
    {
        House houseToUpdate = GetHouseById(houseId);

        if (houseToUpdate.CreatorId != userId)
        {
            throw new Exception($"You are not authorized to update house listing for {houseId}");
        }

        houseToUpdate.Price = houseData.Price ?? houseToUpdate.Price;
        houseToUpdate.Bedrooms = houseData.Bedrooms ?? houseToUpdate.Bedrooms;
        houseToUpdate.Bathrooms = houseData.Bathrooms ?? houseToUpdate.Bathrooms;
        houseToUpdate.Description = houseData.Description ?? houseToUpdate.Description;

        House updatedHouse = _repository.UpdateHouse(houseToUpdate);
        return updatedHouse;
    }
}