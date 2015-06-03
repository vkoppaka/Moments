using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Moments
{
	public class FriendRequestsPage : BasePage
	{
		ListView pendingFriendsListView;
		StackLayout mainLayout;

		public FriendRequestsPage ()
		{
			BindingContext = new FriendRequestsViewModel ();

			SetupUserInterface ();
			SetupEventHandlers ();
			SetupToolbar ();
		}

		private FriendRequestsViewModel ViewModel
		{
			get { return BindingContext as FriendRequestsViewModel; }
		}

		private void SetupUserInterface ()
		{
			pendingFriendsListView = new ListView () {
				BackgroundColor = Color.White,
				IsPullToRefreshEnabled = true,
				ItemsSource = ViewModel.FriendRequests,
				RowHeight = 70
			};

			pendingFriendsListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				pendingFriendsListView.SelectedItem = null;
			};

			var itemTemplate = new DataTemplate (typeof(FriendRequestCell));
			pendingFriendsListView.ItemTemplate = itemTemplate;

			mainLayout = new StackLayout {
				Children = { pendingFriendsListView }
			};

			Content = mainLayout;
		}

		private void SetupEventHandlers ()
		{
			pendingFriendsListView.Refreshing += (sender, e) => {
				pendingFriendsListView.IsRefreshing = true;
				ViewModel.FetchFriendRequestsCommand.Execute (null);
				pendingFriendsListView.IsRefreshing = false;
			};
		}

		private void SetupToolbar ()
		{
			ToolbarItems.Add (new ToolbarItem {
				Icon = Images.CancelButton,
				Command = new Command (() => Navigation.PopModalAsync (true)),
				Priority = 1
			});
		}
	}
}