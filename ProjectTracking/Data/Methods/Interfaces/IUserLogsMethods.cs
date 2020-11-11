using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IUserLogsMethods
    {
        UserLog AddStartLog(string userId, string ipAddress, UserLogStatus status);
        void EndActiveLog(string userId, UserLogStatus status);
        UserLog GetActiveUserLog(string userId);
    }
}