using System.Collections.Generic;
using System.Linq;

namespace ViewModels
{
    public class FloorViewModel
    {
        public int Id { get; set; }
        public int NumOfFloor { get; set; }
        public List<RoomViewModel> Rooms { get; set; }

        public int NumberOfFreeBedsAtThisMoment
        {
            get { return Rooms.Sum(x => x.NumberOfFreeBedsAtThisMoment); }
        }

        public int NumberOfFreeRoomsAtThisMoment
        {
            get { return Rooms.Count(x => x.NumberOfFreeBedsAtThisMoment > 0); }
        }

        public int NumberOfFreeBedsForSelectedPeriod { get { return Rooms.Sum(x => x.NumberOfFreeBedsForSelectedPeriod); } }
        public int NumberOfFreeRoomsForSelectedPeriod { get { return Rooms.Count(x => x.NumberOfFreeBedsForSelectedPeriod > 0); } }
    }
}
