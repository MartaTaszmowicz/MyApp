using System;
using System.Text;
using MyApp.Model;
using MyApp.Repositories;
using MyApp.Services;

namespace MyApp
{
    internal class Program
    {
        const string SEPARATOR =
            ":::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::";

        static void Main(string[] args)
        {
            Console.WriteLine("Manage hotel room availability and reservations.");
            Console.WriteLine("");

            #region Parse input parameters

            Console.WriteLine("Input parameters:");
            Console.WriteLine(" myapp --hotels <hotels.json> --bookings <bookings.json>");
            Console.WriteLine(
                "If the parameters are not specified, the files will be loaded from the /Data directory.");
            Console.WriteLine(SEPARATOR);
            
            string hotelPath = "Data/hotels.json";
            string bookingPath = "Data/bookings.json";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--hotels" && i + 1 < args.Length)
                {
                    hotelPath = args[i + 1];
                }
                else if (args[i] == "--bookings" && i + 1 < args.Length)
                {
                    bookingPath = args[i + 1];
                }
            }

            #endregion // Parse input parameters

            #region Initialise
            Console.WriteLine("[Info] Read bookings from: " + bookingPath);
            BookingRepository bookingRepository = new BookingRepository(bookingPath);
            BookingService bookingService = new BookingService(bookingRepository);

            Console.WriteLine("[Info] Read hotels from: " + hotelPath);
            HotelRepository hotelRepository = new HotelRepository(hotelPath);
            HotelService hotelService = new HotelService(hotelRepository, bookingService);
            Console.WriteLine(SEPARATOR);

            #endregion // Initialise

            #region Commands UI

            Console.WriteLine("Choose an command:");
            Console.WriteLine(" 1 Search");
            Console.WriteLine(" 2 Availability");
            Console.WriteLine(" Any other key to exit.");

            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Example input: Search(H1, 365, SGL) ");
                    Console.Write("Enter search parameters:");
                    userInput = Console.ReadLine();

                    string[]? searchParams = userInput?.Replace("Search(", "").Replace(")", "").Split(',');

                    if (searchParams != null && searchParams.Length == 3)
                    {
                        string hotelId = searchParams[0].Trim();
                        int daysAhead = int.Parse(searchParams[1].Trim());
                        string roomType = searchParams[2].Trim();

                        var hotel = hotelService.GetHotelById(hotelId);
                        if (hotel != null)
                        {
                            if (hotel.id != null)
                            {
                                List<AvailabilityCount>? availability = hotelService.SearchAvailability(hotel.id, daysAhead, roomType);
                                if (availability != null)
                                {
                                    var sb = new StringBuilder();
                                    foreach (var availabilityCount in availability)
                                    {
                                        sb.Append($"({availabilityCount.startDate:yyyyMMdd}-{availabilityCount.endDate:yyyyMMdd},{availabilityCount.count}), ");
                                    }
                                    string result = sb.ToString().TrimEnd(',', ' ');
                                    Console.WriteLine(result);
                                    // or
                                    // foreach (var availabilityCount in availability)
                                    // {
                                    //     Console.WriteLine($"from: {availabilityCount.startDate:yyyy-MM-dd} to: {availabilityCount.endDate:yyyy-MM-dd} available: {availabilityCount.count}");
                                    // }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[Error] HotelId no set.");    
                            }
                        }
                        else
                        {
                            Console.WriteLine("[Error] Hotel not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("[Error] Invalid input format.");
                    }
                    break;
                
                case "2":
                    Console.WriteLine("Example input: ");
                    Console.WriteLine(" Availability(H1, 20240901, SGL)");
                    Console.WriteLine(" Availability(H1, 20240901-20240903, DBL)");
                    Console.Write("Enter availability parameters:");
                    userInput = Console.ReadLine();

                    string[]? availabilityParams = userInput?.Replace("Availability(", "").Replace(")", "").Split(',');
                    if (availabilityParams != null && availabilityParams.Length == 3)
                    {
                        string hotelId = availabilityParams[0].Trim();
                        string roomType = availabilityParams[2].Trim();
                        string dateRange = availabilityParams[1].Trim();

                        string[] dates = dateRange.Split('-');
                        string startDate = dates[0].Trim();
                        string? endDate = dates.Length > 1 ? dates[1].Trim() : null;

                        string str = hotelService.GetRoomAvailability(hotelId, roomType, startDate, endDate);
                        Console.WriteLine(str);
                    }

                    break;
                default:
                    break;
            }
            #endregion // Commands UI

            Console.WriteLine(SEPARATOR);
            Console.WriteLine("Finished.");
        }
    }
}