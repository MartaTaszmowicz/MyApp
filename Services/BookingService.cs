using MyApp.Model;
using MyApp.Repositories;

namespace MyApp.Services;

public class BookingService(BookingRepository bookingRepository)
{
    public List<Booking>? GetAllBookings()
    {
        return bookingRepository.GetAllBookings();
    }
    
    public List<Booking>? GetBookings(string hotelId, string roomType)
    {
        // TODO: Remove addational data, and create DTO on return type
        return bookingRepository.GetBookingsOrderedByArrival(hotelId, roomType);
    }
}