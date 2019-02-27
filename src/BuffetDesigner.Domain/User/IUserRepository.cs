using BuffetDesigner.Domain._Base;

namespace BuffetDesigner.Domain.User
{
    public interface IUserRepository: IRepository<User>
    {
        User GetByEmail(string email);
    }
}