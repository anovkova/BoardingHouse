using System.Collections.Generic;

namespace Domain
{
    public class Room
    {
        public Room() { }
        public virtual int Id { get; set; }
        public virtual Floor Floor { get; set; }
        public virtual int NumOfBeds { get; set; }
        public virtual IList<Rent> Rents { get; set; }
    }
}
