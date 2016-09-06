using System.Collections.Generic;
using System.Linq;

namespace ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public FloorViewModel Floor { get; set; }
        public int NumOfBeds { get; set; }
        public List<RentViewModel> Rents { get; set; }

        public int NumberOfFreeBedsAtThisMoment
        {
            get { return NumOfBeds - Rents.Count(x => x.Active); }
        }

        public int NumberOfFreeBedsForSelectedPeriod { get; set; }
    }
}
