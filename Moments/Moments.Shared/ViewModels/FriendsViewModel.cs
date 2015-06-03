using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Connectivity.Plugin;

namespace Moments
{
	public class FriendsViewModel : BaseViewModel
	{
		ObservableCollection<User> friends;
		Command fetchFriendsCommand;

		public FriendsViewModel ()
		{
			friends = FriendService.Instance.Friends;
		}

		public ObservableCollection<User> Friends
		{
			get { return friends; }
			set { friends = value; } 
		}

		public Command FetchFriendsCommand
		{
			get { return fetchFriendsCommand ?? (fetchFriendsCommand = new Command (async () => await ExecuteFetchFriendsCommand ()));}
		}

		public async Task ExecuteFetchFriendsCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try
			{
				var connected = await CrossConnectivity.Current.IsRemoteReachable (Keys.ApplicationMobileService, 80, 10000);
				if (connected) {
					await FriendService.Instance.RefreshFriendsList ();
				} else {
					DialogService.ShowError (Strings.NoInternetConnection);
				}
			}
			catch (Exception ex) 
			{
				Xamarin.Insights.Report (ex);
			}
				
			IsBusy = false;
		}
	}
}