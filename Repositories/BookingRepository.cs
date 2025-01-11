using MyApp.Model;

namespace MyApp.Repositories;

public class BookingRepository : Repository<Booking>
{
    public BookingRepository(string filePath) : base(filePath)
    {
    }
    
    public List<Booking>? GetAllBookings()
    {
        return Items;
    }

    public List<Booking>? GetBookingsOrderedByArrival(string hotelId, string roomType)
    {
        return Items?
            .Where(b => b.hotelId == hotelId && b.roomType == roomType)
            .OrderBy(b => b.arrival)
            .ToList();
    }
}