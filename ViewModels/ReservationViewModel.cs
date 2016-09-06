using System;

namespace ViewModels
{
    public class ReservationViewModel
    {
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int FloorId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}
