using Domain;
using System.Linq;
using ViewModels;

namespace Mappers
{
    public static class DomainModelMapper
    {
        public static UserViewModel ToUserViewModel(this User user)
        {
            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Embg = user.Embg,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber
            };

            if (user.Role != null)
            {
                model.Role = user.Role.ToRoleViewModel();
            }

            return model;
        }

        public static RoleViewModel ToRoleViewModel(this Role role)
        {
            return new RoleViewModel
            {
                Id = role.Id,
                Title = role.Title
            };
        }
        public static StatusViewModel ToStatusViewModel(this Status status)
        {
            return new StatusViewModel
            {
                Title = status.Title,
                Id = status.Id
            };
        }

        public static AccountViewModel ToAccountViewModel(this Account account)
        {
            var model =  new AccountViewModel
            {
                Id = account.Id,
                Amount = account.Amount,
                Description = account.Description,
                Month = account.Month
            };

            if (account.Status != null)
            {
                model.Status = account.Status.ToStatusViewModel();
                model.Rent = account.Rent.ToRentViewModel();
            }

            return model;
        }

        public static RentViewModel ToRentViewModel(this Rent rent)
        {
            var model = new RentViewModel
            {
                Active = rent.Active,
                DateEnd = rent.DateEnd,
                DateStart = rent.DateStart,
                Id = rent.Id
            };

            if (rent.User != null)
            {
                model.User = rent.User.ToUserViewModel();
            }

            return model;
        }

        public static CurrentRent ToCurrentRentViewModel(this Rent rent)
        {
            var model = new CurrentRent
            {
                DateEnd = rent.DateEnd,
                DateStart = rent.DateStart,
                Id = rent.Id
            };

            if (rent.User != null)
            {
                model.User = rent.User.ToUserViewModel();
            }

            if (rent.Room != null)
            {
                model.RoomId = rent.Room.Id;

                if (rent.Room.Floor != null)
                {
                    model.Floor = rent.Room.Floor.NumOfFloor;
                }
            }

            return model;
        }
        public static RoomViewModel ToRoomViewModel(this Room room)
        {
            return new RoomViewModel
            {
                Id = room.Id,
                NumOfbeds = room.NumOfBeds,
                Rents = (room.Rents != null) ? room.Rents.Select(x => x.ToRentViewModel()).ToList() : null
            };
        }

        public static FloorViewModel ToFloorViewModel(this Floor floor)
        {
          
            return new FloorViewModel
            {
                Id = floor.Id,
                NumOfFloor = floor.NumOfFloor,
                Rooms = floor.Rooms.Select(x => x.ToRoomViewModel()).ToList()
            
             };
        }
    }
}
