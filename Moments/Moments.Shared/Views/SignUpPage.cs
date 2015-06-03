using System;
using Xamarin.Forms;

namespace Moments
{
	public class SignUpPage : BasePage
	{
		Image cancelButton;
		Label signUpLabel;
		MomentsEntry firstNameEntry;
		MomentsEntry lastNameEntry;
		MomentsEntry usernameEntry;
		MomentsEntry passwordEntry;
		MomentsEntry emailEntry;
		Image verticalSeparator;
		Image separator;
		Image separatorTwo;
		Image separatorThree;
		Button signUpButton;
		StackLayout topLayout, firstMiddleRowLayout, bottomLayout, mainLayout;

		TapGestureRecognizer cancelButtonTapped;

		public SignUpPage ()
		{
			BindingContext = new SignUpViewModel ();

			SetupUserInterface ();
			SetupEventHandlers ();
			SetupBindings ();
		}

		private SignUpViewModel ViewModel
		{
			get { return BindingContext as SignUpViewModel; }
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

			signUpLabel = new Label {
				Style = Application.Current.Resources["MainLabelStyle"] as Style,
				Text = Strings.SignUp
			};

			firstNameEntry = new MomentsEntry {
				Keyboard = Keyboard.Text,
				Placeholder = Strings.First,
				HeightRequest = 45,
				WidthRequest = 159
			};

			verticalSeparator = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 45,
				WidthRequest = 1
			};

			lastNameEntry = new MomentsEntry {
				Keyboard = Keyboard.Text,
				Placeholder = Strings.Last,
				HeightRequest = 45,
				WidthRequest = 160
			};

			firstMiddleRowLayout = new StackLayout {
				Children = { firstNameEntry, verticalSeparator, lastNameEntry },
				Spacing = 0,
				Orientation = StackOrientation.Horizontal
			};

			separator = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			usernameEntry = new MomentsEntry {
				Placeholder = Strings.Username,
				HeightRequest = 45
			};

			separatorTwo = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			passwordEntry = new MomentsEntry {
				Placeholder = Strings.Password,
				HeightRequest = 45,
				IsPassword = true
			};

			separatorThree = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			emailEntry = new MomentsEntry {
				Keyboard = Keyboard.Email,
				Placeholder = Strings.Email,
				HeightRequest = 45
			};

			bottomLayout = new StackLayout {
				Children = { firstMiddleRowLayout, separator, usernameEntry, separatorTwo, passwordEntry, separatorThree, emailEntry },
				Spacing = 0
			};

			signUpButton = new Button {
				BackgroundColor = Colors.ButtonBackgroundColor,
				BorderRadius = 0,
				BorderWidth = 0,
				Command = ViewModel.SignUpUserCommand,
				FontAttributes = FontAttributes.None,
				FontFamily = Fonts.HelveticaNeueThin,
				FontSize = 25,
				Text = Strings.SignUp,
				TextColor = Colors.ButtonTextColor
			};

			mainLayout = new StackLayout {
				Children = { topLayout, signUpLabel, bottomLayout, signUpButton },
				Spacing = 25
			};

			Content = mainLayout;
		}

		private void SetupBindings ()
		{
			firstNameEntry.SetBinding (Entry.TextProperty, "FirstName");
			lastNameEntry.SetBinding (Entry.TextProperty, "LastName");
			usernameEntry.SetBinding (Entry.TextProperty, "Username");
			passwordEntry.SetBinding (Entry.TextProperty, "Password");
			emailEntry.SetBinding (Entry.TextProperty, "Email");
		}

		private void SetupEventHandlers ()
		{
			firstNameEntry.Completed += (object sender, EventArgs e) => {
				lastNameEntry.Focus ();
			};

			lastNameEntry.Completed += (object sender, EventArgs e) => {
				usernameEntry.Focus ();
			};

			usernameEntry.Completed += (object sender, EventArgs e) => {
				passwordEntry.Focus ();
			};

			passwordEntry.Completed += (object sender, EventArgs e) => {
				emailEntry.Focus ();
			};

			cancelButtonTapped = new TapGestureRecognizer ();
			cancelButtonTapped.Tapped += (object sender, EventArgs e) => {
				Navigation.PopModalAsync ();
			};
			cancelButton.GestureRecognizers.Add (cancelButtonTapped);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			firstNameEntry.WidthRequest = this.Width / 2;
			lastNameEntry.WidthRequest = this.Width / 2;
		}
	}
}