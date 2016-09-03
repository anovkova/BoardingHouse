using Repository.Interface;
using System.Linq;
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
        public User Login(string email, string password)
        {
            User user = _session.Query<User>().FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);

            return user;
        }
    }
}
