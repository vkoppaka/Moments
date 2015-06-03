using System;
using Xamarin.Forms;
using Refractored.Xam.Settings;
using ImageCircle.Forms.Plugin.Abstractions;

namespace Moments
{
	public class ProfilePage : BasePage
	{
		CircleImage profileImage;
		Label nameLabel;
		Button signOutButton;
		Image separator;
		Button deleteAccountButton;
		Button aboutMomentsButton;

		public ProfilePage ()
		{
			SetupUserInterface ();
			SetupEventHandlers ();
		}

		private void SetupUserInterface ()
		{
			BackgroundColor = Colors.BackgroundColor;
			NavigationPage.SetHasNavigationBar (this, false);

			profileImage = new CircleImage {
				Aspect = Aspect.AspectFill,
				BorderColor = Color.White,
				BorderThickness = 3,
				Source = GetProfileImageUrl (),
				HeightRequest = 125,
				HorizontalOptions = LayoutOptions.Center,
				WidthRequest = 125
			};

			nameLabel = new Label {
				Style = Application.Current.Resources["MainLabelStyle"] as Style,
				Text = GetProfileName ()
			};

			signOutButton = new Button {
				Text = Strings.SignOut
			};

			separator = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			deleteAccountButton = new Button {
				Text = Strings.DeleteAccount
			};

			aboutMomentsButton = new Button {
				Text = Strings.AboutMoments
			};

			var buttonStack = new StackLayout {
				Children = { signOutButton, separator, deleteAccountButton },
				Spacing = 0
			};

			var topStack = new StackLayout {
				Children = { profileImage, nameLabel, buttonStack },
				Spacing = 30,
				Padding = new Thickness (0, 50, 0, 0)
			};

			var mainLayout = new StackLayout {
				Children = { topStack, aboutMomentsButton },
				Spacing = 50
			};

			Content = mainLayout;
		}

		private void SetupEventHandlers ()
		{
			signOutButton.Clicked += (sender, e) => {
				AccountService.Instance.SignOut ();
				App.Current.MainPage = new WelcomePage ();
			};

			deleteAccountButton.Clicked += async (sender, e) => {
				await AccountService.Instance.DeleteAccount ();
				App.Current.MainPage = new WelcomePage ();
			};

			aboutMomentsButton.Clicked += async (sender, e) => {
				var aboutPage = new AboutPage ();
				await App.Current.MainPage.Navigation.PushAsync (aboutPage);
			};
		}

		private string GetProfileImageUrl ()
		{
			return CrossSettings.Current.GetValueOrDefault<string> ("profileImage");
		}

		private string GetProfileName ()
		{
			return CrossSettings.Current.GetValueOrDefault<string> ("profileName");
		}
	}
}