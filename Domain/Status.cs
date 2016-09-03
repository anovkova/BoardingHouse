using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Status
    {
        public Status() { }
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
    }
}
