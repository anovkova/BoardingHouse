using System.Collections.Generic;

namespace ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public FloorViewModel Floor { get; set; }
        public int NumOfbeds { get; set; }
        public  List<RentViewModel> Rents { get; set; }
        public void DeleteRent(RentViewModel rent)
        {
            Rents.Remove(rent);
        }
    }
}
