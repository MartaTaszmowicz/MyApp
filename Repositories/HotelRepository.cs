using MyApp.Model;

namespace MyApp.Repositories;

public class HotelRepository : Repository<Hotel>
{
    public HotelRepository(string filePath) : base(filePath)
    {
    }
    
    public Hotel? GetHotelById(string hotelId)
    {
        return Items?.FirstOrDefault(h => h.id == hotelId);
    }
}