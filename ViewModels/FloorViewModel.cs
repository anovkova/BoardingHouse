using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class FloorViewModel
    {
        public int Id { get; set; }
        public int NumOfFloor { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
           
    }
}
