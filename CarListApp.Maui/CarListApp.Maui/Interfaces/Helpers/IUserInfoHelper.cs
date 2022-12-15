using CarListApp.Maui.Models;

namespace CarListApp.Maui.Interfaces.Helpers;

public interface IUserInfoHelper
{ 
    UserInfo GetUserInfoFromPreferences();
    void SetUserInfoToPreferences(UserInfo userInfo);
}
