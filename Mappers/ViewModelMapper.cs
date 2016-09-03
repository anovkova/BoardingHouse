using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Mappers
{
    public static class ViewModelMapper
    {
        public static Status toStatusDomainModel (this StatusViewModel vm)
        {
            Status status = new Status();
            status.Title = vm.Title;
            status.Id = vm.Id;

            return status;
        }

        public static Role toRoleDomainModel(this RoleViewModel vm)
        {
            Role role = new Role();
            role.Title = vm.Title;
            role.Id = vm.Id;

            return role;
        }

        public static User toUserDomainModel(this UserViewModel vm)
        {
            User user = new User();

            user.Id = vm.Id;
            user.Email = vm.Email;
            user.Embg = vm.Embg;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.Password = vm.Password;
            user.Role = vm.Role.toRoleDomainModel();
            user.PhoneNumber = vm.PhoneNumber;

        
            if (vm.Rents != null)
            {
                foreach (var rent in vm.Rents)
                {
                    user.AddRents(rent.toRentDomainModel());
                }
            }
            

            return user;

        }

        public static Rent toRentDomainModel(this RentViewModel vm)
        {
            Rent rent = new Rent();

            rent.Active = vm.Active;
            rent.DateEnd = vm.DateEnd;
            rent.DateStart = vm.DateStart;
            rent.Id = vm.Id;

            return rent;

        }
    }
}
