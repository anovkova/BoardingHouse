using System;

namespace ViewModels
{
    public class RentSimpleModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int Floor { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
