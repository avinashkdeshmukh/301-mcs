using MT.OnlineRestaurant.DataLayer.Context;

namespace MT.OnlineRestaurant.DataLayer.Interfaces
{
    public interface IUserDataAccess
    {

        TblCustomer UserLogin(string username, string password);
    }
}
