using System;
using Xamarin.Forms;

namespace Moments
{
	public class ViewMomentPage : ContentPage
	{
		Image momentImage;
		StackLayout mainLayout;

		public ViewMomentPage (string image, TimeSpan viewMomentTime)
		{
			BindingContext = new ViewMomentViewModel (image, viewMomentTime);

			SetupUserInterface ();
		}

		private ViewMomentViewModel ViewModel
		{
			get { return BindingContext as ViewMomentViewModel; }
		}

		private void SetupUserInterface ()
		{
			momentImage = new Image {
				Aspect = Aspect.AspectFit,
				Source = ImageSource.FromUri (ViewModel.Image),
				VerticalOptions = LayoutOptions.Center
			};

			momentImage.PropertyChanged += (sender, args) =>
			{
				var img = (Image)sender;

				if (args.PropertyName == "IsLoading" && !img.IsLoading)
				{
					Device.StartTimer (ViewModel.MomentViewTime, () => {
						Navigation.PopModalAsync ();
						return false;
					});
				}
			};

			mainLayout = new StackLayout {
				Children = { momentImage }
			};

			Content = mainLayout;
		}
	}
}