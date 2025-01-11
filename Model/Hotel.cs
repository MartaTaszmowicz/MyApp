using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Model
{
    public class Hotel
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public List<RoomType>? roomTypes { get; set; }
        public List<Room>? rooms { get; set; }
    }
}
