using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RentViewModel
    {
        public int Id { get; set; }
        public RoomViewModel Room { get; set; }
        public UserViewModel User { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool Active { get; set; }
        private List<AccountViewModel> Accounts { get; set; }
    }
}
