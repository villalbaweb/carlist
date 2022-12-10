using CarListApp.Maui.Helpers;
using CarListApp.Maui.Models;

namespace CarListApp.Maui.Controls;

public partial class FlyOutHeader : StackLayout
{
	private readonly UserInfoHelper _userInfoHelper;

	public FlyOutHeader(UserInfoHelper userInfoHelper)
	{
		InitializeComponent();

		_userInfoHelper = userInfoHelper;

		SetValues();
	}

    private void SetValues()
    {
        UserInfo userInfo = _userInfoHelper.GetUserInfoFromPreferences();

		if(userInfo is not null)
		{
			lblUserName.Text = userInfo.Username;
			lblRole.Text = userInfo.Role;
		}
    }
}