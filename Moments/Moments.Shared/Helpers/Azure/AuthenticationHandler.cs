using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refractored.Xam.Settings;

using Akavache;
using System.Reactive.Linq;
using System.Reactive;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments
{
	public class AuthenticationHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);
			response.EnsureSuccessStatusCode();

			var jsonString = await response.Content.ReadAsStringAsync();
			var jsonObject = JObject.Parse(jsonString);
			var token = jsonObject["token"].ToString();
			SaveAuthenticationToken (token);

			return response;
		}

		private void SaveAuthenticationToken (string token)
		{
			AccountService.Instance.AuthenticationToken = token;
			CrossSettings.Current.AddOrUpdateValue<string> ("authenticationKey", token);
			CrossSettings.Current.AddOrUpdateValue<DateTime> ("tokenExpiration", DateTime.Now.AddDays (30));
		}
	}
}