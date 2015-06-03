using System;
using Xamarin.Forms;

namespace Moments
{
	public class SignInPage : BasePage
	{
		Image cancelButton;
		Image separator;
		Label signInLabel;
		MomentsEntry usernameEntry;
		MomentsEntry passwordEntry;
		Button signInButton;
		StackLayout topLayout, middleLayout, mainLayout;

		TapGestureRecognizer cancelButtonTapped;

		public SignInPage ()
		{
			BindingContext = new SignInViewModel ();

			SetupUserInterface ();
			SetupEventHandlers ();
			SetupBindings ();
		}

		private SignInViewModel ViewModel
		{
			get { return BindingContext as SignInViewModel; }
		}

		private void SetupUserInterface ()
		{
			BackgroundColor = Colors.BackgroundColor;
			NavigationPage.SetHasNavigationBar (this, false);

			cancelButton = new Image {
				Source = Images.CancelButton,
				HorizontalOptions = LayoutOptions.Start,
				HeightRequest = 30,
				WidthRequest = 30
			};

			topLayout = new StackLayout {
				Children = { cancelButton },
				Padding = new Thickness (15, 15, 0, 0)
			};

			signInLabel = new Label {
				Style = Application.Current.Resources["MainLabelStyle"] as Style,
				Text = Strings.SignIn
			};

			usernameEntry = new MomentsEntry {
				Placeholder = Strings.Username,
				HeightRequest = 45
			};

			separator = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			passwordEntry = new MomentsEntry {
				Placeholder = Strings.Password,
				HeightRequest = 45,
				IsPassword = true
			};

			middleLayout = new StackLayout {
				Children = { usernameEntry, separator, passwordEntry },
				Spacing = 0
			};
				
			signInButton = new Button {
				BackgroundColor = Colors.ButtonBackgroundColor,
				BorderRadius = 0,
				BorderWidth = 0,
				Command = ViewModel.SignInUserCommand,
				FontAttributes = FontAttributes.None,
				FontFamily = Fonts.HelveticaNeueThin,
				FontSize = 25,
				Text = Strings.SignIn,
				TextColor = Colors.ButtonTextColor
			};

			mainLayout = new StackLayout {
				Children = { topLayout, signInLabel, middleLayout, signInButton },
				Spacing = 25
			};

			Content = mainLayout;
		}

		private void SetupEventHandlers ()
		{
			usernameEntry.Completed += (sender, e) => {
				passwordEntry.Focus ();
			};

			cancelButtonTapped = new TapGestureRecognizer ();
			cancelButtonTapped.Tapped += (object sender, EventArgs e) => {
				Navigation.PopModalAsync ();
			};
			cancelButton.GestureRecognizers.Add (cancelButtonTapped);
		}

		private void SetupBindings ()
		{
			usernameEntry.SetBinding (Entry.TextProperty, "Username");
			passwordEntry.SetBinding (Entry.TextProperty, "Password");
		}
	}
}