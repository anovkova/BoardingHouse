using Domain;
using ViewModels;

namespace Mappers
{
    public static class ViewModelMapper
    {
        public static Status ToStatusDomainModel (this StatusViewModel vm)
        {
            Status status = new Status
            {
                Title = vm.Title,
                Id = vm.Id
            };

            return status;
        }

        public static Role ToRoleDomainModel(this RoleViewModel vm)
        {
            Role role = new Role
            {
                Title = vm.Title,
                Id = vm.Id
            };

            return role;
        }

        public static User ToUserDomainModel(this UserViewModel vm)
        {
            User user = new User
            {
                Id = vm.Id,
                Email = vm.Email,
                Embg = vm.Embg,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Password = vm.Password,
                PhoneNumber = vm.PhoneNumber
            };

            if (vm.Role != null)
            {
                user.Role = vm.Role.ToRoleDomainModel();
            }

            if (vm.Rents != null)
            {
                foreach (var rent in vm.Rents)
                {
                    user.AddRents(rent.ToRentDomainModel());
                }
            }
            
            return user;
        }

        public static Rent ToRentDomainModel(this RentViewModel vm)
        {
            Rent rent = new Rent
            {
                Active = vm.Active,
                DateEnd = vm.DateEnd,
                DateStart = vm.DateStart,
                Id = vm.Id
            };
            
            return rent;
        }
    }
}
