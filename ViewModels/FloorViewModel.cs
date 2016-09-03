using System.Collections.Generic;

namespace ViewModels
{
    public class FloorViewModel
    {
        public int Id { get; set; }
        public int NumOfFloor { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
           
    }
}
