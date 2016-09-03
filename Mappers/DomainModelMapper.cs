using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Mappers
{
    public static class DomainModelMapper
    {
        public static UserViewModel toUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Embg = user.Embg,
                Password = user.Password,
                Role = user.Role.toRoleViewModel(),
                PhoneNumber = user.PhoneNumber
            };
        }
        public static RoleViewModel toRoleViewModel(this Role role)
        {
            return new RoleViewModel
            {
                Id = role.Id,
                Title = role.Title
            };
        }
        public static StatusViewModel toStatusViewModel(this Status status)
        {
            return new StatusViewModel
            {
                Title = status.Title,
                Id = status.Id
            };
        }

        public static AccountViewModel toAccountViewModel(this Account account)
        {
            return new AccountViewModel
            {
                Id = account.Id,
                Amount = account.Amount,
                Description = account.Description,
                Month = account.Month,
                Status = account.Status.toStatusViewModel(),
                Rent = account.Rent.toRentViewModel()
            };
        }

        public static RentViewModel toRentViewModel(this Rent rent)
        {
            return new RentViewModel
            {
                Active = rent.Active,
                DateEnd = rent.DateEnd,
                DateStart = rent.DateStart,
                Id = rent.Id,
                //Room = rent.Room.toRoomViewModel(),
                User = rent.User.toUserViewModel()
                
             
            };
        }

        public static CurrentRent toCurrentRentViewModel(this Rent rent)
        {
            return new CurrentRent
            {
                DateEnd = rent.DateEnd,
                DateStart = rent.DateStart,
                Id = rent.Id,
                RoomId = rent.Room.Id,
                User = rent.User.toUserViewModel(),
                Floor = rent.Room.Floor.NumOfFloor
            };
        }
        public static RoomViewModel toRoomViewModel(this Room room)
        {
            return new RoomViewModel
            {
                Id = room.Id,
                //Floor = room.Floor.toFloorViewModel(),
                //FreeBeds = room.FreeBeds,
                NumOfbeds = room.NumOfbeds,
                Rents = (room.Rents != null) ? room.Rents.Select(x => x.toRentViewModel()).ToList() : null

            };
        }

        public static FloorViewModel toFloorViewModel(this Floor floor)
        {
          
            return new FloorViewModel
            {
                Id = floor.Id,
                NumOfFloor = floor.NumOfFloor,
                Rooms = floor.Rooms.Select(x => x.toRoomViewModel()).ToList()
            
             };
        }
    }
}
