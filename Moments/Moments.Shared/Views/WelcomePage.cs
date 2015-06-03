using System;
using Xamarin.Forms;

namespace Moments
{
	public class WelcomePage : BasePage
	{
		Image momentsIcon;
		Label momentsLabel;
		Button signUpButton;
		Image separator;
		Button signInButton;

		StackLayout momentsStack, buttonStack, mainLayout;

		public WelcomePage ()
		{
			SetupUserInterface ();
			SetupEventHandlers ();
		}

		private void SetupUserInterface ()
		{
			BackgroundColor = Colors.BackgroundColor;
			NavigationPage.SetHasNavigationBar (this, false);

			momentsIcon = new Image {
				Source = Images.MomentsIcon,
				HeightRequest = 162.5,
				WidthRequest = 162.5,
			};

			momentsLabel = new Label {
				Style = Application.Current.Resources["MainLabelStyle"] as Style,
				Text = Strings.Moments
			};

			signUpButton = new Button {
				Text = Strings.SignUp
			};

			separator = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			signInButton = new Button {
				Text = Strings.SignIn
			};

			momentsStack = new StackLayout {
				Children = { momentsIcon, momentsLabel },
				Spacing = 30
			};

			buttonStack = new StackLayout {
				Children = { signUpButton, separator, signInButton },
				Spacing = 0
			};

			mainLayout = new StackLayout {
				Children = { momentsStack, buttonStack },
				Spacing = 35,
				Padding = new Thickness (0, 60, 0, 0)
			};

			Content = mainLayout;
		}

		private void SetupEventHandlers ()
		{
			signUpButton.Clicked += (object sender, EventArgs e) => {
				Navigation.PushModalAsync (new SignUpPage ());
			};

			signInButton.Clicked += (object sender, EventArgs e) => {
				Navigation.PushModalAsync (new SignInPage ());
			};
		}
	}
}