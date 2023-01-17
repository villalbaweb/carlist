using CarListApp.Maui.Controls;
using CarListApp.Maui.Interfaces.Helpers;
using CarListApp.Maui.Models;
using CarListApp.Maui.Views;
using Microsoft.Maui.Controls;

namespace CarListApp.Maui.Helpers;

public class MenuBuildHelper : IMenuBuildHelper
{
    private readonly IUserInfoHelper _userInfoHelper;

    public MenuBuildHelper(IUserInfoHelper userInfoHelper)
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

        FlyoutItem logoutFlyoutItem = new FlyoutItem()
        {
            Title = "Logout",
            Route = nameof(LogoutPage),
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
            Items =
                {
                    new ShellContent
                    {
                        Icon = "dotnet_bot.svg",
                        Title = "Logout",
                        ContentTemplate = new DataTemplate(typeof(LogoutPage))
                    }
                }
        };

        if (!Shell.Current.Items.Contains(logoutFlyoutItem))
        {
            Shell.Current.Items.Add(logoutFlyoutItem);
        }
    }
}
