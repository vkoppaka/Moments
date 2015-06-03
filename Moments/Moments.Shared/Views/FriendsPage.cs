using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Moments
{
	public class FriendsPage : BasePage
	{
		ListView friendsListView;

		public FriendsPage ()
		{
			BindingContext = new FriendsViewModel ();

			SetupToolbar ();
			SetupUserInterface ();
			SetupEventHandlers ();
		}

		private FriendsViewModel ViewModel
		{
			get { return BindingContext as FriendsViewModel; }
		}

		private void SetupToolbar ()
		{
			ToolbarItems.Add (new ToolbarItem {
				Icon = Images.AddFriendButton,
				Command = new Command (() => Navigation.PushModalAsync (new AddFriendPage (), true))
			});

			if (Device.OS != TargetPlatform.iOS) {
				ToolbarItems.Add (new ToolbarItem {
					Icon = Images.FriendRequestsButton,
					Command = new Command (() => Navigation.PushModalAsync (new NavigationPage (new FriendRequestsPage ()) {
						BarBackgroundColor = Colors.NavigationBarColor,
						BarTextColor = Colors.NavigationBarTextColor
					}, true)),
					Priority = 1
				});
			}
		}

		private void SetupUserInterface ()
		{
			friendsListView = new ListView {
				BackgroundColor = Color.White,
				IsPullToRefreshEnabled = true,
				ItemsSource = ViewModel.Friends,
				RowHeight = 70
			};
	
			friendsListView.ItemSelected += (s, e) => {
				friendsListView.SelectedItem = null;
			};

			var itemTemplate = new DataTemplate (typeof(FriendCell));
			friendsListView.ItemTemplate = itemTemplate;

			Content = friendsListView;
		}

		private void SetupEventHandlers ()
		{
			friendsListView.Refreshing += (sender, e) => {
				friendsListView.IsRefreshing = true;
				ViewModel.FetchFriendsCommand.Execute (null);
				friendsListView.IsRefreshing = false;
			};
		}
	}
}