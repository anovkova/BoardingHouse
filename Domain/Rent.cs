using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Domain
{
    public class Rent
    {
        public Rent() { }
        public virtual int Id { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime DateStart { get; set; }
        public virtual DateTime? DateEnd { get; set; }
        public virtual IList<Bill> Bills { get; set; }
        public virtual bool Active
        {
            get
            {
                return DateStart.Date <= DateTime.Now.Date &&
                       (!DateEnd.HasValue || DateEnd.Value.Date >= DateTime.Now.Date);
            }
        }

        public virtual bool FinishedRent
        {
            get { return DateEnd.HasValue && DateEnd.Value.Date < DateTime.Now.Date; }
        }
    }
}
