using CarListApp.Maui.Controls;
using CarListApp.Maui.Models;
using CarListApp.Maui.Views;

namespace CarListApp.Maui.Helpers;

public class MenuBuildHelper
{
    private readonly UserInfoHelper _userInfoHelper;

    public MenuBuildHelper(UserInfoHelper userInfoHelper)
    {
        _userInfoHelper = userInfoHelper;
    }

    public void BuildMenu()
    {
        Shell.Current.Items.Clear();

        Shell.Current.FlyoutHeader = new FlyOutHeader(_userInfoHelper);

        UserInfo userInfo = _userInfoHelper.GetUserInfoFromPreferences();

        if (userInfo.Role.Equals("Administrator"))
        {
            var flyOutItem = new FlyoutItem()
            {
                Title = "Admin Car Management",
                Route = nameof(CarListPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items = 
                {
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "Admin Page 1",
                        ContentTemplate = new DataTemplate(typeof(CarListPage))
                    },
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "Admin Page 2",
                        ContentTemplate = new DataTemplate(typeof(CarListPage))
                    }
                }
            };

            if(!Shell.Current.Items.Contains(flyOutItem))
            {
                Shell.Current.Items.Add(flyOutItem);
            }
        }

        if (userInfo.Role.Equals("User"))
        {
            var flyOutItem = new FlyoutItem()
            {
                Title = "User Car Management",
                Route = nameof(CarListPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "User Page 1",
                        ContentTemplate = new DataTemplate(typeof(CarListPage))
                    },
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "User Page 2",
                        ContentTemplate = new DataTemplate(typeof(CarListPage))
                    }
                }
            };

            if (!Shell.Current.Items.Contains(flyOutItem))
            {
                Shell.Current.Items.Add(flyOutItem);
            }
        }
    }
}
