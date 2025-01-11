using MyApp.Model;
using MyApp.Repositories;

namespace MyApp.Services;

public class HotelService(HotelRepository hotelRepository, BookingService bookingService)
{
    // give the availability count for the specified room type and date range. 
    public string GetRoomAvailability(string hotelId, string roomType, string startDate, string? endDate)
    {
        var bookings = bookingService.GetBookings(hotelId, roomType);
        if (bookings == null || bookings.Count == 0)
        {
            return "[Info] No bookings found.";
        }

        DateTime start = DateTime.ParseExact(startDate, "yyyyMMdd", null);
        DateTime end = endDate != null ? DateTime.ParseExact(endDate, "yyyyMMdd", null) : start;

        int availabilityCount = 0;
        foreach (var booking in bookings)
        {
            if (booking.arrival <= end && booking.departure >= start)
            {
                availabilityCount++;
                break;
            }
        }
        return availabilityCount.ToString();
    }
    
    // Checking room availability in the hotel.
    // Assuming the departure day is the day when the room is free
    public List<AvailabilityCount>? SearchAvailability(string hotelId, int daysAhead, string roomType)
    {
        // Get the number of rooms of the given type in the hotel
        var hotel = hotelRepository.GetHotelById(hotelId);
        if (hotel == null)
        {
            Console.WriteLine("[Error] Hotel not found.");
            return null;
        }

        if (hotel.rooms != null)
        {
            int? roomCount = hotel.rooms.Count(r => r.roomType == roomType);
            if (roomCount == 0 || roomCount == null)
            {
                Console.WriteLine("[Info] No rooms of type " + roomType + " found in hotel: " + hotelId);
                return null;
            }
        
            // A daysAhead-sized board showing what the occupancy is each day.
            var roomAvailability = new int[daysAhead];
            for (int i = 0; i < daysAhead; i++)
            {
                roomAvailability[i] = (int)roomCount;
            }
        
            var bookingsList = bookingService.GetBookings(hotelId, roomType);
        
            // We determine the availability of rooms at the hotel.
            if (bookingsList != null)
                foreach (var booking in bookingsList)
                {
                    DateTime arrivalDate = (DateTime)booking.arrival!;
                    DateTime departureDate = (DateTime)booking.departure!;
                    for (DateTime date = arrivalDate; date < departureDate; date = date.AddDays(1))
                    {
                        var index = (date.AddDays(-1) - DateTime.Today).Days;
                        if (index >= 0 && index < daysAhead)
                        {
                            roomAvailability[index]--;
                        }
                    }
                }

            // We divide the array into ranges according to occupancy.
            var availability = new List<AvailabilityCount>();
            int count = 777;
            for (int i = 0; i < daysAhead; i++)
            {
                count = roomAvailability[i];
                var startDate = DateTime.Today.AddDays(i);
                var endDate = startDate;
                while (i < daysAhead && roomAvailability[i] == count)
                {
                    endDate = DateTime.Today.AddDays(i);
                    i++;
                }
                availability.Add(new AvailabilityCount
                {
                    startDate = startDate,
                    endDate = endDate.AddDays(1),
                    count = (int)count
                });
            }
            return availability;
        }
        return null;
    }
    
    public Hotel? GetHotelById(string hotelId)
    {
        return hotelRepository.GetHotelById(hotelId);
    }
    
}