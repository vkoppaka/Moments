using System;
using Xamarin.Forms;

namespace Moments
{
	public class AboutPage : BasePage
	{
		Image momentsIcon;
		Label momentsLabel;
		Button authorButton;
		Image separator;
		Button locationAboutButton;
		Image separatorTwo;
		Button madeWithXamarinFormsButton;
		Image separatorThree;
		Button buildYourOwnButton;
		Button openSourceLicensureButton;
		Button versionLabel;

		public AboutPage ()
		{
			SetupUserInterface ();
			SetupEventHandlers ();
		}

		private void SetupUserInterface ()
		{
			BackgroundColor = Colors.BackgroundColor;
			Title = Strings.About;

			momentsIcon = new Image {
				Source = Images.MomentsIcon,
				HeightRequest = 162.5,
				WidthRequest = 162.5,
			};

			momentsLabel = new Label {
				Style = Application.Current.Resources["MainLabelStyle"] as Style,
				Text = Strings.Moments
			};

			authorButton = new Button {
				Text = Info.Author,
				FontSize = 20
			};

			separator = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			locationAboutButton = new Button {
				Text = Info.LocationAbout,
				FontSize = 20
			};

			separatorTwo = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			madeWithXamarinFormsButton = new Button {
				Text = Info.MadeWithXamarinForms,
				FontSize = 20
			};

			separatorThree = new Image {
				BackgroundColor = Colors.SeparatorColor,
				HeightRequest = 1,
				WidthRequest = this.Width
			};

			buildYourOwnButton = new Button {
				Text = Strings.BuildYourOwnMoments,
				FontSize = 20
			};

			openSourceLicensureButton = new Button {
				Text = Strings.PoweredByOpenSource,
			    FontSize = 20
			};

			versionLabel = new Button {
				BackgroundColor = Color.Transparent,
				BorderRadius = 0,
				BorderWidth = 0,
				FontAttributes = FontAttributes.None,
				FontFamily = Fonts.HelveticaNeueThin,
				FontSize = 14f,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.EndAndExpand,
				Text = Info.Version,
				TextColor = Color.White
			};

			var momentsStack = new StackLayout {
				Children = { momentsIcon, momentsLabel },
				Spacing = 25
			};

			var buttonStack = new StackLayout {
				Children = { authorButton, separator, locationAboutButton, separatorTwo, madeWithXamarinFormsButton, separatorThree, buildYourOwnButton },
				Spacing = 0
			};

			var mainStack = new StackLayout {
				Children = { momentsStack, buttonStack, openSourceLicensureButton },
				Spacing = 30,
			};

			var mainLayout = new StackLayout {
				Children = { mainStack, versionLabel },
				Padding = new Thickness (0, 60, 0, 0)
			};

			Content = new ScrollView () {
				Content = mainLayout
			};
		}

		private void SetupEventHandlers ()
		{
			authorButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.PierceBogganLink));
			};

			locationAboutButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.AlabamaWikipediaLink));
			};
				
			madeWithXamarinFormsButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.MadeWithXamarinFormsLink));
			};

			buildYourOwnButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.MomentsGitHubLink));
			};

			openSourceLicensureButton.Clicked += async (sender, e) => {
				await Navigation.PushAsync (new WebPage (Strings.PoweredByOpenSourceLink));
			};
		}
	}
}