using CarListApp.Maui.Models;

namespace CarListApp.Maui.Interfaces.Helpers;

public interface IUserInfoHelper
{ 
    void ClearUserInfoPreferences();
    UserInfo GetUserInfoFromPreferences();
    void SetUserInfoToPreferences(UserInfo userInfo);
}
