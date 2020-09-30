using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IUserLogsMethods
    {
        UserLog AddStartLog(string userId, string ipAddress, string comments = null);
    }
}