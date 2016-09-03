using System.Collections.Generic;

namespace Domain
{
    public class Floor
    {
        public Floor() { }
        public virtual int Id { get; set; }
        public virtual int NumOfFloor { get; set; }
        public virtual IList<Room> Rooms { get; set; }
    }
}
