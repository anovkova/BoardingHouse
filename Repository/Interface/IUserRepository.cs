using Domain;

namespace Repository.Interface
{
    public interface IUserRepository 
    {
        User Login(string email, string password);
    }
}
