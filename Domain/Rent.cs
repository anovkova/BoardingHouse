using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Rent
    {
        public Rent() { }
        public virtual int Id { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime? DateStart { get; set; }
        public virtual DateTime? DateEnd { get; set; }
        public virtual bool Active { get; set; }
        public virtual IList<Account> Accounts { get; set; }
    }
}
