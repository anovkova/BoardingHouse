using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Room
    {
        public Room() { }
        public virtual int Id { get; set; }
        public virtual Floor Floor { get; set; }
        public virtual int NumOfbeds { get; set; }
        //public virtual int FreeBeds { get; set; }
        public virtual IList<Rent> Rents { get; set; }
    }
}
