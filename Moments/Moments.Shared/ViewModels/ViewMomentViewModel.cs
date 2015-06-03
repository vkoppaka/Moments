using System;
using System.Net;
using Xamarin.Forms;

namespace Moments
{
	public class ViewMomentViewModel : BaseViewModel
	{
		Uri image;
		TimeSpan momentViewTime;

		public ViewMomentViewModel (string momentUrl, TimeSpan viewTime)
		{
			image = new Uri (momentUrl);
			momentViewTime = viewTime;
		}

		public Uri Image
		{
			get { return image; }
		}

		public TimeSpan MomentViewTime
		{
			get { return momentViewTime; }
		}
	}
}