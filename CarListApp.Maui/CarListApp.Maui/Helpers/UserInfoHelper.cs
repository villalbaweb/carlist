using CarListApp.Maui.Interfaces.Helpers;
using CarListApp.Maui.Models;
using System.Text.Json;

namespace CarListApp.Maui.Helpers;

public class UserInfoHelper : IUserInfoHelper
{
    public void ClearUserInfoPreferences()
    {
        if (Preferences.ContainsKey(nameof(UserInfo)))
        {
            Preferences.Remove(nameof(UserInfo));
        }
    }

    public UserInfo GetUserInfoFromPreferences()
    {
        var userInfoString = Preferences.Get(nameof(UserInfo), null);
        return userInfoString is not null
            ? JsonSerializer.Deserialize<UserInfo>(userInfoString)
            : null;
    }

    public void SetUserInfoToPreferences(UserInfo userInfo)
    {
        if (Preferences.ContainsKey(nameof(UserInfo)))
        {
            Preferences.Remove(nameof(UserInfo));
        }

        string userInfoString = JsonSerializer.Serialize(userInfo);
        Preferences.Set(nameof(UserInfo), userInfoString);
    }
}
