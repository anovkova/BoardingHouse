using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public FloorViewModel Floor { get; set; }
        public int NumOfbeds { get; set; }
        //public int FreeBeds { get; set; }
        public  List<RentViewModel> Rents { get; set; }

        public void deleteRent(RentViewModel rent)
        {
            Rents.Remove(rent);
        }

        
    }
}
