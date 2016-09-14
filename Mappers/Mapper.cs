using Domain;
using System.Linq;
using ViewModels;

namespace Mappers
{
    public static class Mapper
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
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
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

        public static BillViewModel ToAccountViewModel(this Bill bill)
        {
            var model =  new BillViewModel
            {
                Id = bill.Id,
                Amount = bill.Amount,
                Description = bill.Description,
                Month = bill.Month
            };

            if (bill.BillType != null)
            {
                model.BillType = bill.BillType.ToModel();
            }

            if (bill.Status != null)
            {
                model.Status = bill.Status.ToStatusViewModel();
            }

            if (bill.Rent != null)
            {
                model.Rent = bill.Rent.ToRentViewModel();
            }

            return model;
        }

        public static RentViewModel ToRentViewModel(this Rent rent)
        {
            var model = new RentViewModel
            {
                Id = rent.Id,
                Active = rent.Active,
                DateEnd = rent.DateEnd,
                DateStart = rent.DateStart
            };

            if (rent.User != null)
            {
                model.User = rent.User.ToUserViewModel();
            }

            return model;
        }

        public static RentSimpleModel ToSimpleModel(this Rent rent)
        {
            var model = new RentSimpleModel
            {
                DateEnd = rent.DateEnd,
                DateStart = rent.DateStart,
                Id = rent.Id
            };

            if (rent.User != null)
            {
                model.UserFirstName = rent.User.FirstName;
                model.UserLastName = rent.User.LastName;
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
            var model = new RoomViewModel
            {
                Id = room.Id,
                NumOfBeds = room.NumOfBeds
            };

            if (room.Rents != null)
            {
                model.Rents = room.Rents.Select(x => x.ToRentViewModel()).ToList();
            }

            return model;
        }

        public static FloorViewModel ToFloorViewModel(this Floor floor)
        {
            var model = new FloorViewModel
            {
                Id = floor.Id,
                NumOfFloor = floor.NumOfFloor
             };

            if (floor.Rooms != null)
            {
                model.Rooms = floor.Rooms.Select(x => x.ToRoomViewModel()).ToList();
            }

            return model;
        }

        public static BillTypeViewModel ToModel(this BillType type)
        {
            var model = new BillTypeViewModel
            {
                Id = type.Id,
                Name = type.Name
            };

            return model;
        }
    }
}
