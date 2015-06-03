using System;
using Xamarin.Forms;

namespace Moments
{
	public class Styles
	{
		public static void RegisterGlobalStyles ()
		{
			Application.Current.Resources = new ResourceDictionary ();

			var buttonStyle = new Style (typeof(Button)) {
				Setters = {
					new Setter { Property = Button.BackgroundColorProperty, Value = Colors.ButtonBackgroundColor },
					new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
					new Setter { Property = Button.BorderWidthProperty, Value = 0 },
					new Setter { Property = Button.FontAttributesProperty, Value = FontAttributes.None },
					new Setter { Property = Button.FontFamilyProperty, Value = "HelveticaNeue-Thin" },
					new Setter { Property = Button.FontSizeProperty, Value = 25 },
					new Setter { Property = Button.TextColorProperty, Value = Colors.ButtonTextColor }
				}
			};

			var mainLabelStyle = new Style (typeof(Label)) {
				Setters = {
					new Setter { Property = Label.BackgroundColorProperty , Value = Color.Transparent },
					new Setter { Property = Label.FontAttributesProperty , Value = FontAttributes.None },
					new Setter { Property = Label.FontFamilyProperty , Value = "HelveticaNeue-Thin" },
					new Setter { Property = Label.FontSizeProperty , Value = 32f },
					new Setter { Property = Label.HorizontalOptionsProperty , Value = LayoutOptions.Center },
					new Setter { Property = Label.TextColorProperty , Value = Color.White  }
				}
			};

			Application.Current.Resources.Add (buttonStyle);
			Application.Current.Resources.Add ("MainLabelStyle", mainLabelStyle);
		}
	}
}

