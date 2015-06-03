using System;
using Xamarin.Forms;

namespace Moments
{
	public class AddFriendPage : BasePage
	{
		Image cancelButton;
		Label addFriendLabel;
		MomentsEntry usernameEntry;
		Button becomeFriendsButton;
		Layout topLayout, mainLayout;

		TapGestureRecognizer cancelButtonTapped;

		public AddFriendPage ()
		{
			BindingContext = new AddFriendViewModel ();

			SetupUserInterface ();
			SetupEventHandlers ();
			SetupBindings ();
		}

		private AddFriendViewModel ViewModel
		{
			get { return BindingContext as AddFriendViewModel; }
		}

		private void SetupUserInterface ()
		{
			BackgroundColor = Colors.BackgroundColor;
			NavigationPage.SetHasNavigationBar (this, false);

			cancelButton = new Image {
				Source = Images.CancelButton,
				HorizontalOptions = LayoutOptions.Start,
				HeightRequest = 25,
				WidthRequest = 25
			};

			topLayout = new StackLayout {
				Children = { cancelButton },
				Padding = new Thickness (15, 15, 0, 0)
			};

			addFriendLabel = new Label {
				Style = Application.Current.Resources["MainLabelStyle"] as Style,
				Text = Strings.AddAFriend
			};

			usernameEntry = new MomentsEntry {
				Placeholder = Strings.Username,
				HeightRequest = 45
			};

			becomeFriendsButton = new Button {
				BackgroundColor = Colors.ButtonBackgroundColor,
				BorderRadius = 0,
				BorderWidth = 0,
				FontAttributes = FontAttributes.None,
				FontFamily = Fonts.HelveticaNeueThin,
				FontSize = 25,
				Text = Strings.BecomeFriends,
				TextColor = Colors.ButtonTextColor
			};

			mainLayout = new StackLayout {
				Children = { topLayout, addFriendLabel, usernameEntry, becomeFriendsButton },
				Spacing = 25
			};

			Content = mainLayout;
		}

		private void SetupBindings ()
		{
			usernameEntry.SetBinding (Entry.TextProperty, "Username");
		}

		private void SetupEventHandlers ()
		{
			cancelButtonTapped = new TapGestureRecognizer ();
			cancelButtonTapped.Tapped += (object sender, EventArgs e) => {
				Navigation.PopModalAsync ();
			};
			cancelButton.GestureRecognizers.Add (cancelButtonTapped);
		
			becomeFriendsButton.Clicked += (sender, e) => {
				ViewModel.AddFriendCommand.Execute (null);
			};
		}
	}
}