using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class RentViewModel
    {
        public int Id { get; set; }
        public UserViewModel User { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public List<BillViewModel> Accounts { get; set; }
        public bool Active { get; set; }
    }
}
