using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public RoleViewModel Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Embg { get; set; }
        public string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<RentViewModel> Rents { get; set; }

    }
}
