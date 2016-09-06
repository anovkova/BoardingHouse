using Domain;
using Mappers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernates.Repositories;
using ViewModels;

namespace Service
{
    public class UserService
    {
        private IRepository<User> _userRepository;
        private IRepository<Floor> _floorRepository;
        private IRepository<Room> _roomRepository;
        private IRepository<Rent> _rentRepository;
        private IRepository<Role> _roleRepository;

        public UserViewModel Login(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);

                var loginUser = _userRepository.All().FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

                if (loginUser == null)
                    throw new UnauthorizedAccessException();

                return loginUser.ToUserViewModel();
            }
        }

        public List<UserViewModel> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);
                var users =
                    _userRepository.All()
                        .Where(x => x.Role.Id == (int)RoleEnum.User)
                        .Select(x => x.ToUserViewModel())
                        .ToList();

                return users;
            }
        }

        public List<RentSimpleModel> GetAllRents()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _rentRepository = new Repository<Rent>(session);
                var all =
                    _rentRepository.All()
                        .OrderByDescending(x => x.DateStart)
                        .ToList()
                        .Select(x => x.ToSimpleModel()).ToList();

                return all;
            }
        }

        public List<FloorViewModel> GetFloors()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _floorRepository = new Repository<Floor>(session);
                var allFloors = _floorRepository.All().Select(x => x.ToFloorViewModel()).ToList();

                return allFloors;
            }
        }
        
        public void MakeAReservation(ReservationViewModel reservation)
        {
            if (reservation.DateEnd.HasValue && reservation.DateStart.Date > reservation.DateEnd.Value.Date)
                throw new ApplicationException("DATE_START_NOT_VALID");

            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _rentRepository = new Repository<Rent>(session);
                _userRepository = new Repository<User>(session);
                _roomRepository = new Repository<Room>(session);

                var room = _roomRepository.FindBy(reservation.RoomId);
                if (room == null)
                    throw new ApplicationException("ROOM_DOES_NOT_EXIST");
                var numberOfRents = 0;

                foreach (var roomRent in room.Rents)
                {
                    if (OverlappingPeriods(reservation.DateStart, reservation.DateEnd, roomRent.DateStart, roomRent.DateEnd))
                        numberOfRents++;
                }

                if (numberOfRents >= room.NumOfBeds)
                    throw new ApplicationException("NOT_EMPTY_BED");

                var user = _userRepository.FindBy(reservation.UserId);

                if (user == null)
                    throw new ApplicationException("USER_NOT_FOUND");

                foreach (var userRent in user.Rents)
                {
                    if (OverlappingPeriods(reservation.DateStart, reservation.DateEnd, userRent.DateStart, userRent.DateEnd))
                        throw new ApplicationException("USER_ALREADY_HAS_RENT_IN_SELECTED_PERIOD");
                }

                var rent = new Rent
                {
                    User = user,
                    Room = room,
                    DateStart = reservation.DateStart,
                    DateEnd = reservation.DateEnd
                };

                _rentRepository.Add(rent);
                transaction.Commit();

                MailProvider mailProvider = new MailProvider();
                mailProvider.SuccessReservation(rent);
            }
        }

        public void AddUser(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);
                _roleRepository = new Repository<Role>(session);

                if (_userRepository.All().Any(x => x.Email.ToLower() == user.Email.ToLower() && x.Id != user.Id))
                    throw new ApplicationException("EXISTING_USER");

                if (user.Role == null)
                    throw new ApplicationException("ROLE_NOT_SELECTED");

                var role = _roleRepository.FindBy(user.Role.Id);
                if (role == null)
                    throw new ApplicationException("ROLE_DOES_NOT_EXIST");

                var userDomain = _userRepository.FindBy(user.Id) ?? new User();

                userDomain.FirstName = user.FirstName;
                userDomain.LastName = user.LastName;
                userDomain.Password = user.Password;
                userDomain.PhoneNumber = user.PhoneNumber;
                userDomain.Address = user.Address;
                userDomain.Email = user.Email;
                userDomain.Embg = user.Embg;
                userDomain.Role = role;

                _userRepository.AddOrUpdate(userDomain);

                MailProvider mailProvider = new MailProvider();
                mailProvider.SendMailToUser(userDomain);
                transaction.Commit();
            }
        }

        public UserViewModel GetLoginUser(string email)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);

                var user = _userRepository.All().FirstOrDefault(x => x.Email == email);

                if (user == null)
                    throw new ApplicationException("USER_NOT_FOUND");

                return user.ToUserViewModel();
            }
        }

        public RentSimpleModel GetCurrentRentByUser(UserViewModel model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                var user = _userRepository.FindBy(model.Id);
                if (user == null)
                    throw new ApplicationException("USER_NOT_FOUND");

                var activeRent = user.Rents.FirstOrDefault(x => x.Active);

                return activeRent != null ? activeRent.ToSimpleModel() : null;
            }
        }

        public List<RentSimpleModel> GetAllRentByUser(UserViewModel model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);

                var user = _userRepository.FindBy(model.Id);

                if (user == null)
                    throw new ApplicationException("USER_NOT_FOUND");

                return user.Rents.Select(x => x.ToSimpleModel()).ToList();
            }
        }

        public List<FloorViewModel> SearchFreeFloors(ReservationViewModel model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (session.BeginTransaction())
            {
                _floorRepository = new Repository<Floor>(session);
                _userRepository = new Repository<User>(session);
                var floors = _floorRepository.All().Select(x => x.ToFloorViewModel()).ToList();
                var user = _userRepository.FindBy(model.UserId);

                if(user == null)
                    throw new ApplicationException("USER_NOT_FOUND");

                foreach (var rent in user.Rents)
                {
                    if(OverlappingPeriods(model.DateStart, model.DateEnd, rent.DateStart, rent.DateEnd))
                        return new List<FloorViewModel>();
                }
                
                foreach (var floor in floors.ToList())
                {
                    foreach (var room in floor.Rooms.ToList())
                    {
                        var numberOfRents = 0;
                        foreach (var rent in room.Rents.ToList())
                        {
                            if (OverlappingPeriods(model.DateStart, model.DateEnd, rent.DateStart, rent.DateEnd))
                                numberOfRents++;
                        }

                        if (numberOfRents >= room.NumOfBeds)
                        {
                            floor.Rooms.Remove(room);
                        }
                        else
                        {
                            room.NumberOfFreeBedsForSelectedPeriod = room.NumOfBeds - numberOfRents;
                        }
                    }
                }

                foreach (var floor in floors.ToList())
                {
                    if (floor.Rooms == null || floor.Rooms.Count == 0)
                    {
                        floors.Remove(floor);
                    }
                }

                return floors;
            }
        }

        private bool OverlappingPeriods(DateTime rentStartDate, DateTime? rentEndDate, DateTime startDate, DateTime? endDate)
        {
            if (!rentEndDate.HasValue)
            {
                if (!endDate.HasValue)
                    return true;

                return rentStartDate.Date <= endDate.Value;
            }

            if (!endDate.HasValue)
            {
                return rentEndDate.Value.Date >= startDate.Date;
            }

            return (startDate.Date >= rentStartDate.Date && startDate.Date <= rentEndDate.Value.Date) ||
                    (endDate.Value.Date >= rentStartDate.Date && endDate.Value.Date <= rentEndDate.Value.Date) ||
                    (startDate.Date <= rentStartDate.Date && endDate.Value.Date >= rentEndDate.Value.Date);
        }
    }
}
