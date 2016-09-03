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

        public UserService()
        {

        }
      
        public UserViewModel Login(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);

                var users = _userRepository.All().ToList();
                User loginUser = _userRepository.All().Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();

                _floorRepository = new Repository<Floor>(session);
                var floors = _floorRepository.All().ToList();
                Floor floor1 = _floorRepository.FindBy(1);

                _roomRepository = new Repository<Room>(session);
                var rooms = _roomRepository.All().ToList();
                Room room1 = _roomRepository.FindBy(1);

                _rentRepository = new Repository<Rent>(session);
                var rent1 = _rentRepository.FindBy(1);

                if (loginUser != null)
                    return loginUser.ToUserViewModel();
                else return null;
            }
       

        }

        public List<UserViewModel> getAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);
                var users = _userRepository.All().Where(x => x.Role.Id == 2).ToList();

                return users.Select(x => x.ToUserViewModel()).ToList();
            }
          
        }

        public List<CurrentRent> GetAllRents()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _rentRepository = new Repository<Rent>(session);
                var all = _rentRepository.All().ToList();
                return all.Select(x => x.ToCurrentRentViewModel()).OrderByDescending(x => x.DateStart).ToList();
            }

        }
        
        public List<FloorViewModel> GetFloors()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _floorRepository = new Repository<Floor>(session);
                var allFloors =  _floorRepository.All();
                var floors = allFloors.Select(x => x.ToFloorViewModel()).ToList();
 
                return floors;
            }
        }

        public FloorViewModel GetFloorsByNumOfFloor(FloorViewModel floor)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _floorRepository = new Repository<Floor>(session);        
                var floorView = _floorRepository.All().Where(x => x.NumOfFloor == floor.NumOfFloor).FirstOrDefault().ToFloorViewModel();

                foreach (var room in floorView.Rooms)
                {
                    foreach (var rent in room.Rents.ToList())
                        if (!CheckRent(rent))
                        {
                            room.DeleteRent(rent);
                        }
                }
            
                return floorView;
            }
        }

        public List<RoomViewModel> ActiveRents(List<RoomViewModel> rooms)
        {
            foreach(var room in rooms)
            {
                foreach (var rent in room.Rents)
                    if (!CheckRent(rent))
                        room.Rents.Remove(rent);  
            }
            return rooms;
        }

        public List<RoomViewModel> GetFreeRoom(ReservationViewModel resrvation)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _floorRepository = new Repository<Floor>(session);
                var floor = _floorRepository.FindBy(resrvation.FloorId);

                List<RoomViewModel> availableRooms = new List<RoomViewModel>();
                if (resrvation.DateEnd > resrvation.DateStart && resrvation.DateStart.Date>= DateTime.Now.Date)
                {
                    foreach (var room in floor.Rooms.ToList())
                    {
                        int count = 0;
                        foreach (var rent in room.Rents.ToList())
                        {
                            if (InValidRent(rent.DateStart.Value, rent.DateEnd.Value, resrvation.DateStart, resrvation.DateEnd))
                               count++;
                            if (rent.DateEnd < resrvation.DateStart)
                                room.Rents.Remove(rent);
                        }
                        if (count < room.NumOfBeds)
                            availableRooms.Add(room.ToRoomViewModel());       
                    }
                    return availableRooms;
                }
                return null;
               
            }
        }

        public bool makeAReservation(ReservationViewModel reservation)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _rentRepository = new Repository<Rent>(session);
                _userRepository = new Repository<User>(session);
                _roomRepository = new Repository<Room>(session);

                Rent rent = new Rent();
                rent.User = _userRepository.FindBy(reservation.UserId);
                rent.DateEnd = reservation.DateEnd;
                rent.DateStart = reservation.DateStart;
                rent.Room = _roomRepository.FindBy(reservation.RoomId);

                var usersRent = _rentRepository.All().Where(x => x.User.Id == reservation.UserId).ToList();
                bool flag = true;
                foreach(var tmpRent in usersRent)
                {
                    if (InValidRent(tmpRent.DateStart.Value, tmpRent.DateEnd.Value, reservation.DateStart, reservation.DateEnd))
                        flag = false;
                }
                if (flag)
                {
                    _rentRepository.Add(rent);
                    transaction.Commit();
                    
                    MailProvider mailProvider = new MailProvider();
                    mailProvider.SuccessReservation(rent);
                  
                }

                return flag;
            

            }
        }

        public bool InValidRent(DateTime rentStartDate, DateTime rentEndDate, DateTime startDate, DateTime endDate)
        {
            return ((startDate.Date >= rentStartDate.Date && startDate.Date <= rentEndDate.Date) || 
                    (endDate.Date >= rentStartDate.Date && endDate.Date <= rentEndDate.Date) ||
                    (startDate.Date <= rentStartDate.Date && endDate.Date >= rentEndDate.Date));
        }

        public bool CheckRent(RentViewModel rent)
        {
            DateTime current = DateTime.Now;
         
            if (rent.DateStart > current || rent.DateEnd < current)
                    return false;            
            return true;
        }

        public void AddUser(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);
                _roleRepository = new Repository<Role>(session);
                _rentRepository = new Repository<Rent>(session);


                if (_userRepository.All().Any(x => x.Email.ToLower() == user.Email.ToLower() && x.Id != user.Id))
                    throw new ApplicationException("Existing user");

                //user.Rents = new List<RentViewModel>();

                //foreach (var rent in _rentRepository.All().Where(x => x.User.Id == user.Id).ToList())
                //    user.Rents.Add(rent.toRentViewModel());

                //var userDomain = user.toUserDomainModel();
                if (user.Role == null)
                    throw new ApplicationException("Role must be selected");
                var role = _roleRepository.FindBy(user.Role.Id);
                if (role == null)
                    throw new ApplicationException("Role does not exist");

                var userDomain = _userRepository.FindBy(user.Id) ?? new User();

                userDomain.FirstName = user.FirstName;
                userDomain.LastName = user.LastName;
                userDomain.Password = user.Password;
                userDomain.PhoneNumber = user.PhoneNumber;
                userDomain.Email = user.Email;
                userDomain.Embg = user.Embg;
                userDomain.Role = role;

                _userRepository.AddOrUpdate(userDomain);

                MailProvider mailProvider = new MailProvider();
                mailProvider.SendMailToUser(userDomain);
                transaction.Commit();
            }
        }

        public List<UserViewModel> UpdateUser(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);
                _roleRepository = new Repository<Role>(session);



                var userDb = _userRepository.All().Where(x => x.Email == user.Email).FirstOrDefault();
                if (userDb != null)
                {
                    _userRepository.Update(user.ToUserDomainModel());
                    transaction.Commit();
                }

                return getAllUsers();
            }
            return null;
        }

        public UserViewModel GetLoginUser(string email)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _userRepository = new Repository<User>(session);

                var user = _userRepository.All().Where(x => x.Email == email).FirstOrDefault();

                if (user != null)
                    return user.ToUserViewModel();

                return null;
            }
        }

        public CurrentRent GetCurrentRentByUser(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _rentRepository = new Repository<Rent>(session);
                DateTime now = DateTime.Now;

               return  _rentRepository.All().Where(x => x.User == user.ToUserDomainModel() && now >= x.DateStart && now <= x.DateEnd).SingleOrDefault().ToCurrentRentViewModel();


            }
        }

        public List<CurrentRent> GetAllRentByUser(UserViewModel user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                _rentRepository = new Repository<Rent>(session);
                DateTime now = DateTime.Now;

                var rents= _rentRepository.All().Where(x => x.User == user.ToUserDomainModel() && now > x.DateEnd).ToList();

                return rents.Select(x => x.ToCurrentRentViewModel()).ToList();


            }
        }

    }
}
