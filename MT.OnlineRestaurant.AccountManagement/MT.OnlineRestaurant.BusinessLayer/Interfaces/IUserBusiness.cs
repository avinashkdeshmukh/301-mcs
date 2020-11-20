using MT.OnlineRestaurant.DataLayer.Context;

namespace MT.OnlineRestaurant.BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {

        TblCustomer UserLogin(string username, string password);
    }
}
