﻿using System.Collections.Generic;

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
        public virtual string Address { get; set; }
        public string Password { get; set; }
        public List<RentViewModel> Rents { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

    }
}
