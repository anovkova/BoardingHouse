using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public User() {
           
        }
        public virtual int Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Embg { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Password { get; set; }
        public virtual  IList<Rent> Rents { get; set; }

        public virtual void AddRents(Rent rent)
        {
            this.Rents.Add(rent);
        }
    }
}
