using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using Acr.UserDialogs;
using Connectivity.Plugin;

namespace Moments
{
	public class AddFriendViewModel : BaseViewModel
	{
		string username;
		Command addFriendCommand;

		public string Username
		{
			get { return username; }
			set { username = value; OnPropertyChanged ("Username"); }
		}

		public Command AddFriendCommand
		{
			get { return addFriendCommand ?? (addFriendCommand = new Command (async () => await ExecuteAddFriendCommand ())); }
		}

		private async Task ExecuteAddFriendCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try
			{
				var connected = await CrossConnectivity.Current.IsRemoteReachable (Keys.ApplicationMobileService, 80, 10000);
				if (connected) {
					DialogService.ShowLoading (Strings.AddingFriend);
					var success = await CreateFriendship ();
					DialogService.HideLoading ();
					if (success) {
						DialogService.ShowSuccess (Strings.FriendAdded);
					} else {
						DialogService.ShowError (Strings.FriendRequestFailed);
					}
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

		private async Task<bool> CreateFriendship ()
		{
			return await FriendService.Instance.CreateFriendship (Username);
		}
	}
}