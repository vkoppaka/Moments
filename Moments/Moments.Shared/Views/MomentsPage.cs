using System;
using Xamarin.Forms;

namespace Moments
{
	public class MomentsPage : BasePage
	{
		ListView momentsListView;

		public MomentsPage ()
		{
			BindingContext = new MomentsViewModel ();

			SetupUserInterface ();
			SetupEventHandlers ();
		}

		private MomentsViewModel ViewModel
		{
			get { return BindingContext as MomentsViewModel; }
		}

		private void SetupUserInterface ()
		{
			var itemTemplate = new DataTemplate (typeof(MomentCell));

			momentsListView = new ListView {
				BackgroundColor = Color.White,
				IsPullToRefreshEnabled = true,
				ItemTemplate = itemTemplate,
				ItemsSource = ViewModel.Moments,
				RowHeight = 70
			};

			momentsListView.ItemSelected += (s, e) => {
				momentsListView.SelectedItem = null;
			};

			momentsListView.ItemTapped += (s, e) => {
				var moment = e.Item as Moment;
				if (moment == null) {
					return;
				}

				App.Current.MainPage.Navigation.PushModalAsync (new ViewMomentPage (moment.MomentUrl, TimeSpan.FromSeconds (moment.DisplayTime)));
				ViewModel.DestroyImageCommand.Execute (moment);
				momentsListView.SelectedItem = null;
			};

			Content = momentsListView;
		}

		private void SetupEventHandlers ()
		{
			momentsListView.Refreshing += (sender, e) => {
				momentsListView.IsRefreshing = true;
				ViewModel.FetchMomentsCommand.Execute (null);
				momentsListView.IsRefreshing = false;
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (ViewModel == null || ViewModel.IsBusy || ViewModel.Moments.Count > 0) {
				return;
			}

			ViewModel.FetchMomentsCommand.Execute (null);
		}
	}
}