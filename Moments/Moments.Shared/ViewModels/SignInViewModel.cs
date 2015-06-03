using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using Connectivity.Plugin;

namespace Moments
{
	public class SignInViewModel : BaseViewModel
	{
		string username;
		string password;

		Command logInUserCommand;

		public string Username
		{
			get { return username; }
			set { username = value; OnPropertyChanged ("Username"); }
		}

		public string Password
		{
			get { return password; }
			set { password = value; OnPropertyChanged ("Password"); }
		}

		public Command SignInUserCommand
		{
			get { return logInUserCommand ?? (logInUserCommand = new Command (async () => await ExecuteSignInUserCommand ())); }
		}

		private async Task ExecuteSignInUserCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try
			{
				var connected = await CrossConnectivity.Current.IsRemoteReachable (Keys.ApplicationMobileService, 80, 10000);
				if (connected) {
					DialogService.ShowLoading (Strings.SigningIn);
					var result = await SignIn ();
					DialogService.HideLoading ();

					if (result) {
						NavigateToMainUI ();
					} else {
						DialogService.ShowError (Strings.InvalidCredentials);
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

		private async Task<bool> SignIn ()
		{
			var account = new Account {
				Username = Username,
				Password = Password
			};

			return await AccountService.Instance.Login (account);
		}

		private void NavigateToMainUI ()
		{
			App.Current.MainPage = App.FetchMainUI ();
		}
	}
}