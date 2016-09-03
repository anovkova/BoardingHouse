using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NHibernate;
using NHibernate.Linq;
namespace Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }
        public User Login(String email, String password)
        {
            User user = _session.Query<User>().Where(x => x.Email == email && x.Password == password).FirstOrDefault();

            return user;


        }
    }
}
