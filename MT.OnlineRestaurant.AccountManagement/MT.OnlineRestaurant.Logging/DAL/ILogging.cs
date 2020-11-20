#region References
using MT.OnlineRestaurant.Logging.Context;
#endregion

#region namespace
namespace MT.OnlineRestaurant.Logging.DAL
{
    #region interface definition
    /// <summary>
    /// method definition to capture the action activities
    /// </summary>
    public interface ILogging
    {
        void CaptureLogs(LoggingInfo loggingInfo);
    }
    #endregion
}
#endregion
