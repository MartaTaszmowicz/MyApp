using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class Booking
    {
        public string? hotelId { get; set; }
        public DateTime? arrival { get; set; }
        public DateTime? departure { get; set; }
        public string? roomType { get; set; }
        public string? roomRate { get; set; }
    }
}
