using System;

namespace ViewModels
{
    public class CurrentRent
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Floor { get; set; }
        public UserViewModel User { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
