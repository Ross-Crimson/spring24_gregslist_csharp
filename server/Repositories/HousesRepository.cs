




namespace csharp_gregslist_api.Repositories;

public class HousesRepository
{
    private readonly IDbConnection _db;

    public HousesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal House CreateHouse(House houseData)
    {
        string sql = @"INSERT INTO
        houses (
            sqft,
            bedrooms,
            bathrooms,
            imgUrl,
            description,
            price,
            creatorId
        )
        VALUES(
        @Sqft,
            @Bedrooms,
            @Bathrooms,
            @ImgUrl,
            @Description,
            @Price,
            @CreatorId 
        );
        
        SELECT * FROM houses WHERE id = LAST_INSERT_ID();";

        House house = _db.Query<House>(sql, houseData).FirstOrDefault();

        return house;
    }

    internal void DestroyHouse(int houseId)
    {
        string sql = "DELETE FROM houses WHERE id = @houseId";

        _db.Execute(sql, new { houseId });
    }

    internal House GetHouseById(int houseId)
    {
        string sql = "Select * FROM houses WHERE id = @houseId;";

        House house = _db.Query<House>(sql, new { houseId }).FirstOrDefault();
        return house;
    }

    internal List<House> GetHouses()
    {
        string sql = "SELECT * FROM houses;";

        List<House> houses = _db.Query<House>(sql).ToList();
        return houses;
    }

    internal House UpdateHouse(House houseToUpdate)
    {
        string sql = @"UPDATE houses
        SET
        price = @Price,
        bedrooms = @Bedrooms,
        bathrooms = @Bathrooms,
        description = @Description
        WHERE id = @id;
        
        SELECT
        houses.*,
        accounts.*
        FROM houses
        JOIN accounts ON accounts.id = houses.creatorId
        WHERE houses.id = @id";

        House house = _db.Query<House, Account, House>(sql, (house, account) =>
        {
            house.Creator = account;
            return house;
        }, houseToUpdate).FirstOrDefault();

        return house;
    }
}