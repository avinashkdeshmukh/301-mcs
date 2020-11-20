namespace MT.OnlineRestaurant.ActionHandler
{
    public interface IActionHandler
    {
        int Handle(object payload);
    }
}