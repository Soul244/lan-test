using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Client
{
	public class Bitis : ContentPage
	{
        private Label label = new Label
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            Text="Sorular Bitti"
        };
        public Bitis ()
		{
			Content = new StackLayout {
				Children = {
					label
				}
			};
		}
	}
}