using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Moments
{
	public class SendMomentPage : BasePage
	{
		ListView friendsListView;

		public SendMomentPage (Stream image, int displayTime)
		{
			BindingContext = new SendMomentViewModel (this, image, displayTime);

			SetupToolbar ();
			SetupUserInterface ();
		}

		private SendMomentViewModel ViewModel
		{
			get { return BindingContext as SendMomentViewModel; }
		}

		private void SetupToolbar ()
		{
			ToolbarItems.Add (new ToolbarItem {
				Text = Strings.Send,
				Priority = 1,
				Command = ViewModel.SendMomentCommand
			});
		}

		private void SetupUserInterface ()
		{
			var template = new DataTemplate (typeof(SendMomentCell));

			friendsListView = new ListView {
				BackgroundColor = Color.White,
				ItemsSource = ViewModel.Friends,
				ItemTemplate = template,
				RowHeight = 70
			};

			friendsListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				friendsListView.SelectedItem = null;
			};

			Content = friendsListView;
		}
	}
}