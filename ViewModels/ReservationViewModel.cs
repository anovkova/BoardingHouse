using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ReservationViewModel
    {
        //public UserViewModel User { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        //public FloorViewModel Floor { get; set; }
        public int floorId { get; set; }
        public int userId { get; set; }
        public int roomId { get; set; }




    }
}
