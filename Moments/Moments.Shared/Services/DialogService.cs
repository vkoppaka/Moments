using System;
using Acr.UserDialogs;

namespace Moments
{
	public class DialogService
	{
		public static void ShowLoading ()
		{
			UserDialogs.Instance.ShowLoading ();
		}

		public static void ShowLoading (string loadingMessage)
		{
			UserDialogs.Instance.ShowLoading (loadingMessage);
		}

		public static void HideLoading ()
		{
			UserDialogs.Instance.HideLoading ();
		}

		public static void ShowError (string errorMessage)
		{
			UserDialogs.Instance.ShowError (errorMessage);
		}

		public static void ShowSuccess (string successMessage)
		{
			UserDialogs.Instance.ShowSuccess (successMessage);
		}

		public static void ShowSuccess (string successMessage, int timeOut)
		{
			UserDialogs.Instance.ShowSuccess (successMessage, timeOut);
		}
	}
}

